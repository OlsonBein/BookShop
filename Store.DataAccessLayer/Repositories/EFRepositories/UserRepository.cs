using Microsoft.AspNetCore.Identity;
using Store.DataAccessLayer.AppContext;
using Store.DataAccessLayer.Common.Extensions;
using Store.DataAccessLayer.Models.Filter;
using Store.DataAccessLayer.Models.Response;
using Store.DataAccessLayer.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Store.DataAccessLayer.Common.Constants.Constants;
using static Store.DataAccessLayer.Common.Enums.Filter.Enums;

namespace Store.DataAccessLayer.Repositories.EFRepositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationContext _dbContext;

        public UserRepository(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ApplicationContext dbContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _dbContext = dbContext;
        }

        public async Task<bool> CreateAsync(ApplicationUser applicationUser, string password)
        {
            var result = await _userManager.CreateAsync(applicationUser, password);
            return result.Succeeded;
        }

        public async Task<bool> DeleteAsync(ApplicationUser applicationUser)
        {            
            applicationUser.IsRemoved = true;
            var result = await _userManager.UpdateAsync(applicationUser);
            return result.Succeeded;
        }

        public async Task<ApplicationUser> GetByIdAsync(long id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            return user;
        }

        public async Task<bool> UpdateAsync(ApplicationUser applicationUser)
        {
            var result = await _userManager.UpdateAsync(applicationUser);
            return result.Succeeded; 
        }

        public async Task<ApplicationUser> GetByEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return user;
        }

        public async Task<bool> AddToRoleAsync(ApplicationUser applicationUser)
        {
            var result = await _userManager.AddToRoleAsync(applicationUser, RoleConstants.User);
            return result.Succeeded;
        }

        public async Task<bool> LogInAsync(ApplicationUser applicationUser, string password)
        {
            var result = await _signInManager.CheckPasswordSignInAsync(applicationUser, password, lockoutOnFailure: false);           
            return result.Succeeded;
        }

        public async Task LogOutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<bool> CheckPasswordAsync(ApplicationUser applicationUser, string password)
        {
            var result = await _signInManager.CheckPasswordSignInAsync(applicationUser, password, lockoutOnFailure: false);
            return result.Succeeded;
        }

        public async Task<string> GenerateEmailConfirmationTokenAsync(ApplicationUser applicationUser)
        {
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(applicationUser);
            return code;
        }

        public async Task<bool> ConfirmEmailAsync(ApplicationUser applicationUser, string token)
        {
            var result = await _userManager.ConfirmEmailAsync(applicationUser, token);
            return result.Succeeded;
        }

        public async Task<bool> IsEmailConfirmedAsync(ApplicationUser applicationUser)
        {
            return await _userManager.IsEmailConfirmedAsync(applicationUser);           
        }

        public async Task<ResponseModel<List<UserResponseModel>>> GetFilteredAsync(UsersFilterModel model, long adminId)
        {
            var substring = model.SearchString.Split(" ");
            var users = _dbContext.Users.Where(x => !x.IsRemoved)
                .Where(x => x.Id != adminId).AsQueryable();
            if (model.SearchString != null)
            {
                users = users.Where(x => x.FirstName.Contains(substring[0]) ||
                    x.Email.Contains(model.SearchString) ||
                    x.LastName.Contains(substring[0]));
            }
            if (model.BlockState == FilterUserBlock.Active)
            {
                users = users.Where(x => x.LockoutEnd == null);
            }
            if (model.BlockState == FilterUserBlock.Blocked)
            {
                users = users.Where(x => x.LockoutEnd != null);
            }
            var sortedUsers = users.AsEnumerable().SortByProperty(model.FilteredColumnType.ToString(), model.SortType);
            var count = sortedUsers.Count();
            var responseUsers = sortedUsers
                .Skip((model.PageCount - 1) * model.PageSize).Take(model.PageSize)
                .GroupBy(x => x.Id)
                .Select(group => new UserResponseModel
                { 
                    User = group.Select(x => x).FirstOrDefault()
                }).ToList();
            var result = new ResponseModel<List<UserResponseModel>>(responseUsers, count);
            return result;
        }

        public async Task<string> GetRoleAsync(ApplicationUser applicationUser)
        {
            var roles = await _userManager.GetRolesAsync(applicationUser);
            return roles.FirstOrDefault();
        }

        public async Task<bool> BlockUserAsync(ApplicationUser applicationUser, DateTime? blockTime)
        {
            var result = await _userManager.SetLockoutEndDateAsync(applicationUser, blockTime);
            return result.Succeeded;
        }

        public async Task<bool> ResetPasswordAsync(ApplicationUser applicationUser, string newPassword)
        {
            var token = await _userManager.GeneratePasswordResetTokenAsync(applicationUser);
            var result = await _userManager.ResetPasswordAsync(applicationUser, token, newPassword);
            return result.Succeeded;
        }

        public async Task<bool> IsLockedOutAsync(ApplicationUser applicationUser)
        {
            return await _userManager.IsLockedOutAsync(applicationUser);
        }

        public async Task<bool> ChangePasswordAsync(ApplicationUser user, string oldPassword, string newPassword)
        {
            var result = await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);
            return result.Succeeded;
        }
    }
}


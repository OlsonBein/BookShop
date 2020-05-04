using Store.DataAccessLayer.Models.Filter;
using Store.DataAccessLayer.Models.Response;
using Store.DataAccessLayer.Repositories.EFRepositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store.DataAccessLayer.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> CreateAsync(ApplicationUser applicationUser, string password);
        Task<bool> DeleteAsync(ApplicationUser applicationUser);
        Task<ApplicationUser> GetByIdAsync(long id);
        Task<bool> UpdateAsync(ApplicationUser applicationUser);
        Task<bool> AddToRoleAsync(ApplicationUser applicationUser);
        Task<bool> LogInAsync(ApplicationUser applicationUser, string password);
        Task LogOutAsync();
        Task<ApplicationUser> GetByEmailAsync(string email);
        Task<string> GenerateEmailConfirmationTokenAsync(ApplicationUser applicationUser);
        Task<bool> CheckPasswordAsync(ApplicationUser applicationUser, string password);
        Task<bool> ConfirmEmailAsync(ApplicationUser applicationUser, string token);
        Task<bool> IsEmailConfirmedAsync(ApplicationUser applicationUser);
        Task<ResponseModel<List<UserResponseModel>>> GetFilteredAsync(UsersFilterModel model, long adminId);
        Task<string> GetRoleAsync(ApplicationUser applicationUser);
        Task<bool> BlockUserAsync(ApplicationUser applicationUser, DateTime? blockTime);
        Task<bool> ResetPasswordAsync(ApplicationUser applicationUser, string newPassword);
        Task<bool> IsLockedOutAsync(ApplicationUser applicationUser);
        Task<bool> ChangePasswordAsync(ApplicationUser user, string oldPassword, string newPassword);
    }
}

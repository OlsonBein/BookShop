using Store.BusinessLogicLayer.Mappers;
using Store.BusinessLogicLayer.Models.Base;
using Store.BusinessLogicLayer.Models.Users;
using Store.BusinessLogicLayer.Services.Interfaces;
using Store.DataAccessLayer.Models.Response;
using Store.DataAccessLayer.Repositories.EFRepositories;
using Store.DataAccessLayer.Repositories.Interfaces;
using System;
using System.Threading.Tasks;
using static Store.BusinessLogicLayer.Common.Constants.Error.Constants;
using static Store.DataAccessLayer.Common.Constants.Constants;
using UserBlockEnum = Store.BusinessLogicLayer.Models.Enums.Filter.Enums.UserStatus;

namespace Store.BusinessLogicLayer.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }        

        public async Task<UserModelItem> GetByIdAsync(long id)
        {
            var response = new UserModelItem();
            var applicationUser = await _userRepository.GetByIdAsync(id);
            if (applicationUser == null)
            {
                response.Errors.Add(ErrorConstants.ImpossibleToFindUser);
                return response;
            }
            return UserMapper.MapEntityToModel(applicationUser);
        }

        public async Task<UserModel> GetAllAsync(UsersFilterModel model, long adminId)
        {
            var response = new UserModel();
            if (model == null)
            {
                response.Errors.Add(ErrorConstants.ModelIsNull);
                return response;
            }
            var filterModel = FilterMapper.MapUserFilteringModel(model);
            var applicationUsers = await _userRepository.GetFilteredAsync(filterModel, adminId);           
            var users = new UserModel();
            foreach (var user in applicationUsers.Data)
            {
                var userModelItem = await UpdateStatus(user);
                users.Items.Add(userModelItem);
            }
            users.TotalCount = applicationUsers.TotalItemsCount;
            return users;           
        }

        private async Task<UserModelItem> UpdateStatus(UserResponseModel user)
        {
            var userStatus = await _userRepository.IsLockedOutAsync(user.User);
            var userModelItem = UserMapper.MapResponseModelToModelItem(user);
            var result = userStatus ? userModelItem.Status = UserBlockEnum.Blocked : 
                userModelItem.Status = UserBlockEnum.Active;
            return userModelItem;
        }

        public async Task<BaseModel> EditAsync(UserModelItem model, long adminId)
        {
            var response = new BaseModel();
            var existingUser = await _userRepository.GetByIdAsync(model.Id);
            if (existingUser == null)
            {
                response.Errors.Add(ErrorConstants.ImpossibleToFindUser);
                return response;
            }
            var admin = await _userRepository.GetByIdAsync(adminId);
            if (admin == null)
            {
                response.Errors.Add(ErrorConstants.ImpossibleToFindUser);
                return response;
            }
            var adminRole = await _userRepository.GetRoleAsync(admin);
            var userRole = await _userRepository.GetRoleAsync(existingUser);
            var updatedUser = new ApplicationUser();
            if (adminRole == RoleConstants.Admin)
            {
                updatedUser = UserMapper.EditEntityByAdminRole(existingUser, model);
            }            
            if (adminRole == userRole)
            {
                updatedUser = UserMapper.EditEntityByUserRole(existingUser, model);
            }
            var result = await _userRepository.UpdateAsync(updatedUser);
            if (!result)
            {
                response.Errors.Add(ErrorConstants.ImpossibleToUpdateUser);
                return response;
            }
            if (!string.IsNullOrWhiteSpace(model.NewPassword) && adminRole == userRole)
            {
                result = await _userRepository.ChangePasswordAsync(updatedUser, model.OldPassword, model.NewPassword);
            }
            if (!result)
            {
                response.Errors.Add(ErrorConstants.ImpossibleToChangeUserPassword);
            }
            return response;
        }


        public async Task<BaseModel> DeleteAsync(long id)
        {
            var response = new BaseModel();
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                response.Errors.Add(ErrorConstants.ImpossibleToFindUser);
                return response;
            }
            var result = await _userRepository.DeleteAsync(user);
            if (!result)
            {
                response.Errors.Add(ErrorConstants.ImpossibleToDeleteUser);
            }
            return response;
        }

        public async Task<BaseModel> BlockAsync(long id, bool block)
        {
            var response = new BaseModel();
            var applicationUser = await _userRepository.GetByIdAsync(id);
            if (applicationUser == null)
            {
                response.Errors.Add(ErrorConstants.ImpossibleToFindUser);
                return response;
            }
            var result = await _userRepository.BlockUserAsync(applicationUser, block ? DateTime.MaxValue : (DateTime?)null);
            if (!result)
            {
                response.Errors.Add(ErrorConstants.ImpossibleToBlockUser);
            }
            return response;
        }

        public async Task<BaseModel> SetPhotoAsync(UserModelItem model)
        {
            var user = await _userRepository.GetByIdAsync(model.Id);
            user.Avatar = model.Avatar;
            var result = await _userRepository.UpdateAsync(user);
            if (!result)
            {
                model.Errors.Add(ErrorConstants.ImpossibleToSetPhoto);
            }
            return model;
        }
    }
}

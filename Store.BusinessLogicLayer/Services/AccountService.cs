using Store.BusinessLogicLayer.Helpers.Interfaces;
using Store.BusinessLogicLayer.Mappers;
using Store.BusinessLogicLayer.Models.Account;
using Store.BusinessLogicLayer.Models.Base;
using Store.BusinessLogicLayer.Models.Users;
using Store.BusinessLogicLayer.Services.Interfaces;
using Store.DataAccessLayer.Repositories.Interfaces;
using System.Threading.Tasks;
using static Store.BusinessLogicLayer.Common.Constants.Email.Constants;
using static Store.BusinessLogicLayer.Common.Constants.Error.Constants;

namespace Store.BusinessLogicLayer.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUserRepository _userRepository;
        private readonly IEmailHelper _emailHelper;
        private readonly IPasswordHelper _passwordHelper;

        public AccountService(IUserRepository userRepository, IEmailHelper emailHelper, IPasswordHelper passwordHelper)
        {
            _userRepository = userRepository;
            _emailHelper = emailHelper;
            _passwordHelper = passwordHelper;
        }

        public async Task<BaseModel> RegisterAsync(RegistrationModel model)
        {
            var response = new BaseModel();
            if (model == null)
            {
                response.Errors.Add(ErrorConstants.ModelIsNull);
                return response;
            }
            var existingUser = await _userRepository.GetByEmailAsync(model.Email);
            if (existingUser != null)
            {
                response.Errors.Add(ErrorConstants.UserWithTheSameNameExists);
                return response;
            }
            var applicationUser = UserMapper.MapRegistrationModelToEntity(model);
            var result = await _userRepository.CreateAsync(applicationUser, model.Password);
            if (!result)
            {
                response.Errors.Add(ErrorConstants.ImpossibleToCreateUser);
                return response;
            }
            result = await _userRepository.AddToRoleAsync(applicationUser);
            if (!result)
            {
                response.Errors.Add(ErrorConstants.ImpossibleToAddUserToRole);
            }
            return response;
        }

        public async Task<UserModelItem> LogInAsync(LogInModel model, string password)
        {
            var response = new UserModelItem();
            if (model == null)
            {
                response.Errors.Add(ErrorConstants.ModelIsNull);
                return response;
            }
            var existingUser = await _userRepository.GetByEmailAsync(model.Email);
            if (existingUser == null)
            {
                response.Errors.Add(ErrorConstants.IncorrectEmail);
                return response;
            }
            var result = await _userRepository.IsEmailConfirmedAsync(existingUser);
            if (!result)
            {
                response.Errors.Add(ErrorConstants.DontConfirmEmail);
                return response;
            }
            var role = await _userRepository.GetRoleAsync(existingUser);
            response = UserMapper.MapEntityToModel(existingUser);
            response.Role = role;         
            result = await _userRepository.IsLockedOutAsync(existingUser);
            if (result)
            {
                response.Errors.Add(ErrorConstants.UserIsLocked);
                return response;
            }
            result = await _userRepository.LogInAsync(existingUser, password);
            if (!result)
            {
                response.Errors.Add(ErrorConstants.IncorrectPassword);
            }
            return response;
        }

        public async Task LogOutAsync()
        {          
            await _userRepository.LogOutAsync();
        }

        public async Task<string> GenerateEmailConfirmationTokenAsync(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return string.Empty;
            }
            var existingUser = await _userRepository.GetByEmailAsync(email);
            if (existingUser == null)
            {
                return string.Empty;
            }
            return await _userRepository.GenerateEmailConfirmationTokenAsync(existingUser);       
        }

        public async Task<UserModelItem> GetUserByEmailAsync(string email)
        {
            var response = new UserModelItem();
            var existingUser = await _userRepository.GetByEmailAsync(email);
            if (existingUser == null)
            {
                response.Errors.Add(ErrorConstants.ImpossibleToFindUser);
                return response;
            }
            var userModel = await MapUserAndGetRoleAsync(existingUser.Id);
            return userModel;
        }

        public async Task<UserModelItem> GetUserByIdAsync(long id)
        {
            var response = new UserModelItem();
            var existingUser =  await _userRepository.GetByIdAsync(id);
            if (existingUser == null)
            {
                response.Errors.Add(ErrorConstants.ImpossibleToFindUser);
                return response;
            }
            var userModel = await MapUserAndGetRoleAsync(id);
            return userModel;
        }

        private async Task<UserModelItem> MapUserAndGetRoleAsync(long id)
        {
            var existingUser = await _userRepository.GetByIdAsync(id);
            var userModel = UserMapper.MapEntityToModel(existingUser);
            var userRoles = await _userRepository.GetRoleAsync(existingUser);
            userModel.Role = userRoles;
            return userModel;
        }
        public async Task<BaseModel> SendMailAsync(string userEmailToConfirm, string token, string subject)
        {
            var response = new BaseModel();
            var result = await _emailHelper.SendMessageAsync(userEmailToConfirm, token, subject);
            if (!result)
            {
                response.Errors.Add(ErrorConstants.ImpossibleToSendMessage);
            }
            return response;
        }

        public async Task<BaseModel> ConfirmEmailAsync(long id, string token)
        {
            var response = new BaseModel();
            var existingUser = await _userRepository.GetByIdAsync(id);
            if (existingUser == null)
            {
                response.Errors.Add(ErrorConstants.ImpossibleToFindUser);
                return response;
            }
            var result = await _userRepository.ConfirmEmailAsync(existingUser, token);
            if (!result)
            {
                response.Errors.Add(ErrorConstants.ImpossibleToConfirmEmail);
            }
            return response;
        }

        public async Task<BaseModel> IsEmailConfirmedAsync(string email)
        {
            var response = new BaseModel();
            var existingUser = await _userRepository.GetByEmailAsync(email);
            if (existingUser == null)
            {
                response.Errors.Add(ErrorConstants.ImpossibleToFindUser);
                return response;
            }
            var result = await _userRepository.IsEmailConfirmedAsync(existingUser);
            if (!result)
            {
                response.Errors.Add(ErrorConstants.DontConfirmEmail);
            }
            return response;
        }

        public async Task<BaseModel> ResetPasswordAsync(string email)
        {
            var response = new BaseModel();
            if (string.IsNullOrWhiteSpace(email))
            {
                response.Errors.Add(ErrorConstants.EmptyEmailField);
                return response;
            }
            var existingUser = await _userRepository.GetByEmailAsync(email);
            if (existingUser == null)
            {
                response.Errors.Add(ErrorConstants.ImpossibleToFindUser);
                return response;
            }
            var newPassword = _passwordHelper.GenerateRandomPassword();
            var result = await _userRepository.ResetPasswordAsync(existingUser, newPassword);
            if (!result)
            {
                response.Errors.Add(ErrorConstants.ImpossibleToResetPassword);
            }
            result = await _emailHelper.SendMessageAsync(email, newPassword, EmailConstants.NewPasswordSubject);
            if (!result)
            {
                response.Errors.Add(ErrorConstants.ImpossibleToSendMessage);
            }
            return response;
        }

        public async Task<BaseModel> IsLockedOutAsync(UserModelItem model)
        {
            var response = new BaseModel();
            if (model == null)
            {
                response.Errors.Add(ErrorConstants.ModelIsNull);
                return response;
            }
            var user = UserMapper.MapModelToEntity(model);
            var result =await _userRepository.IsLockedOutAsync(user);
            if (result)
            {
                response.Errors.Add(ErrorConstants.UserIsLocked);
            }
            return response;
        }
    }
}

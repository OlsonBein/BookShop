using Store.BusinessLogicLayer.Models.Account;
using Store.BusinessLogicLayer.Models.Base;
using Store.BusinessLogicLayer.Models.Users;
using System.Threading.Tasks;

namespace Store.BusinessLogicLayer.Services.Interfaces
{
    public interface IAccountService
    {
        Task<BaseModel> RegisterAsync(RegistrationModel user);
        Task<UserModelItem> LogInAsync(LogInModel userModel, string password);
        Task LogOutAsync();
        Task<string> GenerateEmailConfirmationTokenAsync(string email);
        Task<BaseModel> SendMailAsync(string userEmailToConfirm, string token, string subject);
        Task<UserModelItem> GetUserByEmailAsync(string email);
        Task<UserModelItem> GetUserByIdAsync(long id);
        Task<BaseModel> ConfirmEmailAsync(long id, string token);
        Task<BaseModel> IsEmailConfirmedAsync(string email);
        Task<BaseModel> ResetPasswordAsync(string email);
        Task<BaseModel> IsLockedOutAsync(UserModelItem model);
    }
}

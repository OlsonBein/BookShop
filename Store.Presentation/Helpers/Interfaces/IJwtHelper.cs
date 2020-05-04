using Microsoft.IdentityModel.Tokens;
using Store.BusinessLogicLayer.Models.Account;
using Store.BusinessLogicLayer.Models.Users;

namespace Store.Presentation.Helpers.Interfaces
{
    public interface IJwtHelper
    {
        SecurityKey GetKey();
        TokensModel GenerateTokens(UserModelItem model);
        TokensModel RefreshOldTokens(UserModelItem model, string refreshToken);
        long GetIdFromToken(string token);
        string GetRoleFromToken(string token);
    }
}

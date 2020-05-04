using Store.BusinessLogicLayer.Models.Base;

namespace Store.BusinessLogicLayer.Models.Account
{
    public class TokensModel : BaseModel
    {
        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }
    }
}

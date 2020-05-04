using System.ComponentModel.DataAnnotations;

namespace Store.BusinessLogicLayer.Models.Account
{
    public class LogInModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}

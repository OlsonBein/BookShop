using Microsoft.AspNetCore.Http;
using Store.BusinessLogicLayer.Models.Base;
using static Store.BusinessLogicLayer.Models.Enums.Filter.Enums;

namespace Store.BusinessLogicLayer.Models.Users
{
    public class UserModelItem : BaseModel
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string Role { get; set; }
        public UserStatus Status { get; set; }
        public string Avatar { get; set; }
    }
}

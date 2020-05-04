using Store.BusinessLogicLayer.Models.Base;
using static Store.BusinessLogicLayer.Models.Enums.Filter.Enums;

namespace Store.BusinessLogicLayer.Models.Users
{
    public class UsersFilterModel : BaseFilterModel
    {
        public UserStatus Status { get; set; }
        public UserFilteredColumnType FilteredColumnType { get; set; }
    }   
}

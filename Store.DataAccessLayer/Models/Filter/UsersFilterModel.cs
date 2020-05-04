using static Store.DataAccessLayer.Common.Enums.Filter.Enums;

namespace Store.DataAccessLayer.Models.Filter
{
    public class UsersFilterModel : BaseFilterModel
    {
        public FilterUserBlock BlockState { get; set; }
        public FilterUserByColumn FilteredColumnType { get; set; }
    }
}

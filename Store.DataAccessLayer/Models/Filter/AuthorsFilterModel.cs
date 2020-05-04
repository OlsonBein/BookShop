using static Store.DataAccessLayer.Common.Enums.Filter.Enums;

namespace Store.DataAccessLayer.Models.Filter
{
    public class AuthorsFilterModel : BaseFilterModel
    {
        public FilterAuthorByColumn FilteredColumnType { get; set; }
    }
}

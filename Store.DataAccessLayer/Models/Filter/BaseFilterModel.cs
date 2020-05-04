using static Store.DataAccessLayer.Common.Enums.Filter.Enums;

namespace Store.DataAccessLayer.Models.Filter
{
    public class BaseFilterModel
    {
        public AscendingDescendingSort SortType { get; set; }
        public string SearchString { get; set; }
        public int PageCount { get; set; }
        public int PageSize { get; set; }
    }
}

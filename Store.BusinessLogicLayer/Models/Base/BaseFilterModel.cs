using static Store.BusinessLogicLayer.Models.Enums.Filter.Enums;

namespace Store.BusinessLogicLayer.Models.Base
{
    public class BaseFilterModel
    {
        public FilterAscendingDescending SortType { get; set; }
        public string SearchString { get; set; }
        public int PageCount { get; set; }
        public int PageSize { get; set; }
    }
}

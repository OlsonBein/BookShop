using Store.BusinessLogicLayer.Models.Base;
using static Store.BusinessLogicLayer.Models.Enums.Filter.Enums;

namespace Store.BusinessLogicLayer.Models.Authors
{
    public class AuthorsFilteringModel : BaseFilterModel
    {
        public AuthorFilteredColumnType FilteredColumnType { get; set; }
    }
}

using static Store.DataAccessLayer.Common.Enums.Filter.Enums;

namespace Store.DataAccessLayer.Models.Filter
{
    public class OrdersFilterModel : BaseFilterModel
    {
        public FilterOrdersByColumns FilteredColumnType { get; set; }
    }
}

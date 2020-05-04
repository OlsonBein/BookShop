using Store.BusinessLogicLayer.Models.Base;
using static Store.BusinessLogicLayer.Models.Enums.Filter.Enums;

namespace Store.BusinessLogicLayer.Models.Orders
{
    public class OrdersFilterModel : BaseFilterModel
    {
        public OrderFilteredColumnType FilteredColumnType { get; set; }
    }
}

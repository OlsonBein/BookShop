using Store.BusinessLogicLayer.Models.Base;
using System.Collections.Generic;

namespace Store.BusinessLogicLayer.Models.Orders
{
    public class OrdersModel : BaseModel
    {
        public ICollection<OrdersModelItem> Items { get; set; } = new List<OrdersModelItem>();
        public int TotalCount { get; set; }
    }
}

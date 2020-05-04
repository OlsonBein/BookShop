using Store.BusinessLogicLayer.Models.Base;
using System.Collections.Generic;

namespace Store.BusinessLogicLayer.Models.OrderItem
{
    public class OrderItemModel : BaseModel
    {
        public ICollection<OrderItemModelItem> Items { get; set; } = new List<OrderItemModelItem>();
        public int TotalCount { get; set; }
    }
}

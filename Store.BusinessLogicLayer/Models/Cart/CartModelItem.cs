using Store.BusinessLogicLayer.Models.Base;
using Store.BusinessLogicLayer.Models.OrderItem;
using System.Collections.Generic;

namespace Store.BusinessLogicLayer.Models.Cart
{
    public class CartModelItem
    {
        public List<OrderItemModelItem> OrderItems { get; set; }
        public string Description { get; set; }
        public string TransactionId { get; set; }
        public decimal TotalAmount { get; set; }
    }
}

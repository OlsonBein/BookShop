using Store.BusinessLogicLayer.Models.Base;
using Store.BusinessLogicLayer.Models.OrderItem;
using System;
using System.Collections.Generic;
using static Store.BusinessLogicLayer.Models.Enums.Entity.Enums;

namespace Store.BusinessLogicLayer.Models.Orders
{
    public class OrdersModelItem : BaseModel
    {
        public long Id { get; set; }
        public string PaymentDate { get; set; }
        public string UserName { get; set; }
        public long UserId { get; set; }
        public string  UserEmail { get; set; }
        public List<OrderItemModelItem> OrderItems { get; set; }
        public StatusType OrderStatus { get; set; }
        public int Count { get; set; }
        public decimal TotalAmount { get; set; }
        public long PaymentId { get; set; }
        public string Description { get; set; }
    }
}

using Store.BusinessLogicLayer.Models.Base;
using System;
using static Store.BusinessLogicLayer.Models.Enums.Entity.Enums;

namespace Store.BusinessLogicLayer.Models.OrderItem
{
    public class OrderItemModelItem : BaseModel
    {
        public long PrintingEditionId { get; set; }
        public long OrderId { get; set; }
        public ProductType ProductType { get; set; }
        public string ProductTitle { get; set; }
        public int Count { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal Amount { get; set; }
    }
}

using Store.DataAccessLayer.Repositories.EFRepositories;
using System;
using System.Collections.Generic;
using static Store.DataAccessLayer.Common.Enums.Entity.Enums;

namespace Store.DataAccessLayer.Models.Response
{
    public class OrderResponseModel
    {
        public List<OrderItem> OrderItems { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public DateTime OrderDate { get; set; }
        public long OrderId { get; set; }
        public decimal Amount { get; set; }
        public StatusType OrderStatus { get; set; }
        public string Description { get; set; }
    }
}

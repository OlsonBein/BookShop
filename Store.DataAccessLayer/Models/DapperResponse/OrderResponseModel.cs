using System;
using static Store.DataAccessLayer.Common.Enums.Entity.Enums;

namespace Store.DataAccessLayer.Models.DapperResponse
{
    public class OrderResponseModel
    {
        public long Id { get; set; }
        public DateTime CreationDate { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public ProductType ProductType { get; set; }
        public string ProductTitle { get; set; }
        public int Count { get; set; }
        public decimal Amount { get; set; }
        public StatusType Status { get; set; }
    }
}

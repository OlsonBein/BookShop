using Dapper.Contrib.Extensions;
using Store.DataAccessLayer.Repositories.Base;
using System.ComponentModel.DataAnnotations.Schema;
using static Store.DataAccessLayer.Common.Enums.Entity.Enums;

namespace Store.DataAccessLayer.Repositories.EFRepositories
{
    public class OrderItem : BaseEntity
    {
        public decimal Amount { get; set; }
        public Currency Currency { get; set; }
        public long PrintingEditionId { get; set; }

        [ForeignKey("PrintingEditionId")]
        [Write(false)]
        public PrintingEdition PrintingEdition { get; set; }
        public long OrderId { get; set; }

        [ForeignKey("OrderId")]
        [Write(false)]
        public Order Orders { get; set; }
        public int Count { get; set; }
    }
}

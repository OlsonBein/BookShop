using Dapper.Contrib.Extensions;
using Store.DataAccessLayer.Repositories.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Store.DataAccessLayer.Repositories.EFRepositories
{
    public class Order : BaseEntity
    {
        public string Description { get; set; }
        public long UserId { get; set; }

        [ForeignKey("UserId")]
        [Write(false)]
        public ApplicationUser User { get; set; }
        public long PaymentId { get; set; }

        [ForeignKey("PaymentId")]
        [Write(false)]
        public Payment Payment { get; set; }

        [Write(false)]
        public List<OrderItem> OrderItems { get; set; }

    }
}

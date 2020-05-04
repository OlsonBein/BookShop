using Dapper.Contrib.Extensions;
using Store.DataAccessLayer.Repositories.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using static Store.DataAccessLayer.Common.Enums.Entity.Enums;

namespace Store.DataAccessLayer.Repositories.EFRepositories
{
    public class PrintingEdition : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public StatusType Status { get; set; }
        public Currency Currency { get; set; }
        public ProductType Type { get; set; }

        [NotMapped]
        [Write(false)]
        public ICollection<AuthorInPrintingEdition> AuthorInPrintingEditions { get; set; }
    }
}

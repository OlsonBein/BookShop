using Store.BusinessLogicLayer.Models.Authors;
using Store.BusinessLogicLayer.Models.Base;
using System.Collections.Generic;
using static Store.BusinessLogicLayer.Models.Enums.Entity.Enums;

namespace Store.BusinessLogicLayer.Models.PrintingEdition
{
    public class PrintingEditionsModelItem : BaseModel
    {
        public string Title { get; set; }
        public long Id { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public StatusType Status { get; set; }
        public Currency Currency { get; set; }
        public ProductType ProductType { get; set; }
        public int Sale { get; set; }
        public ICollection<AuthorsModelItem> Authors { get; set; }

        public PrintingEditionsModelItem()
        {
            Authors = new List<AuthorsModelItem>();
        }
    }
}

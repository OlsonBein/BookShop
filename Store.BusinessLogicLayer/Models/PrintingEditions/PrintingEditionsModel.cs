using Store.BusinessLogicLayer.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Store.BusinessLogicLayer.Models.PrintingEdition
{
    public class PrintingEditionsModel : BaseModel
    {
        public ICollection<PrintingEditionsModelItem> Items { get; set; } = new List<PrintingEditionsModelItem>();
        public int TotalCount { get; set; }
    }
}

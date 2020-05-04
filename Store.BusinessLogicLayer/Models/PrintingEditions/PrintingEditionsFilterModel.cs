using Store.BusinessLogicLayer.Models.Base;
using System.Collections.Generic;
using static Store.BusinessLogicLayer.Models.Enums.Entity.Enums;
using static Store.BusinessLogicLayer.Models.Enums.Filter.Enums;

namespace Store.BusinessLogicLayer.Models.PrintingEdition
{
    public class PrintingEditionsFilterModel : BaseFilterModel
    {
        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }
        public List<ProductType> ProductTypes { get; set; }
        public ProductFilteredColumnType FilteredColumnType { get; set; }
        public Currency Currency { get; set; }
    }
}

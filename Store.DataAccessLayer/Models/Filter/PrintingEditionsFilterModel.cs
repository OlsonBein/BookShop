using System.Collections.Generic;
using static Store.DataAccessLayer.Common.Enums.Entity.Enums;
using static Store.DataAccessLayer.Common.Enums.Filter.Enums;

namespace Store.DataAccessLayer.Models.Filter
{
    public class PrintingEditionsFilterModel : BaseFilterModel
    {
        public decimal MinPrice { get; set; }

        public decimal MaxPrice { get; set; }

        public List<ProductType> ProductType { get; set; } = new List<ProductType>();

        public FilterProductByColumns FilteredColumnType { get; set; }
    }
}

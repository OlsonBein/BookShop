using static Store.DataAccessLayer.Common.Enums.Entity.Enums;

namespace Store.DataAccessLayer.Models.DapperResponse
{
    public class PrintingEditionsResponseModel
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ProductType Type { get; set; }
        public decimal Price { get; set; }
        public string AuthorName { get; set; }
    }
}

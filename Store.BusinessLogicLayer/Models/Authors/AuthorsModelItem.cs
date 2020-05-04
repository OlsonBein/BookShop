using Store.BusinessLogicLayer.Models.Base;
using System.Collections.Generic;

namespace Store.BusinessLogicLayer.Models.Authors
{
    public class AuthorsModelItem : BaseModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public ICollection<string> PrintingEditionTitles { get; set; } = new List<string>();
    }
}

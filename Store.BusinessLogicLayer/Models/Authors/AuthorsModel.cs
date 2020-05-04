using Store.BusinessLogicLayer.Models.Base;
using System.Collections.Generic;

namespace Store.BusinessLogicLayer.Models.Authors
{
    public class AuthorsModel : BaseModel
    {
        public ICollection<AuthorsModelItem> Items { get; set; } = new List<AuthorsModelItem>();
        public int TotalCount { get; set; }
    }
}

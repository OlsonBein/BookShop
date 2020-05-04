using Store.BusinessLogicLayer.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Store.BusinessLogicLayer.Models.Users
{
    public class UserModel : BaseModel
    {
        public ICollection<UserModelItem> Items { get; set; } = new List<UserModelItem>();
        public int TotalCount { get; set; }
    }
}

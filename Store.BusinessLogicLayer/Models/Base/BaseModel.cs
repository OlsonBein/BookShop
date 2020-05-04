using System;
using System.Collections.Generic;
using System.Text;

namespace Store.BusinessLogicLayer.Models.Base
{
     public class BaseModel
     {
       public ICollection<string> Errors { get; set; } = new List<string>();
     }
}

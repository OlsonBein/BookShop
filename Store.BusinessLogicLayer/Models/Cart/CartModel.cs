using Store.BusinessLogicLayer.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Store.BusinessLogicLayer.Models.Cart
{
    public class CartModel
    {
        public ICollection<CartModelItem> Items { get; set; } = new List<CartModelItem>();
        public int TotalCount { get; set; }
    }
}

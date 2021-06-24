using System;
using System.Collections.Generic;
using System.Text;

namespace Basket.DataAccess.Models
{
    public class ShoppingCart
    {
        public int UserId { get; set; }

        public List<ShoppingCartItem> Items { get; set; }
    }
}

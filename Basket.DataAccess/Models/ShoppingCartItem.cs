using System;
using System.Collections.Generic;
using System.Text;

namespace Basket.DataAccess.Models
{
    public class ShoppingCartItem
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public int Quantity { get; set; }
    }
}

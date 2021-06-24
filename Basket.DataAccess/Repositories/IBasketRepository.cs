using Basket.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Basket.DataAccess.Repositories
{
    public interface IBasketRepository
    {
        public Task<ShoppingCart> UpdateShoppingCart(ShoppingCart shoppingCart);

        public Task<ShoppingCart> GetShoppingCart(int userId);
    }
}

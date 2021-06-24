using Basket.DataAccess.Models;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Basket.DataAccess.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDistributedCache _cacheDb;

        public BasketRepository(IDistributedCache cacheDb)
        {
            _cacheDb = cacheDb;
        }

        public async Task<ShoppingCart> GetShoppingCart(int userId)
        {
            var shoppingCart = await _cacheDb.GetStringAsync(userId.ToString());

            if (shoppingCart == null)
            {
                return null;
            }

            return JsonConvert.DeserializeObject<ShoppingCart>(shoppingCart);
        }

        public async Task<ShoppingCart> UpdateShoppingCart(ShoppingCart shoppingCart)
        {
            await _cacheDb.SetStringAsync(shoppingCart.UserId.ToString(), JsonConvert.SerializeObject(shoppingCart));

            return await GetShoppingCart(shoppingCart.UserId);
        }
    }
}

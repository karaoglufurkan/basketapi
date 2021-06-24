using Basket.Common.Surrogates;
using Basket.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Business.Services
{
    public interface IBasketService
    {
        Task<ShoppingCart> AddToBasket(AddProductDto productInfo);

        Task<ShoppingCart> GetBasket(int userId);
    }
}

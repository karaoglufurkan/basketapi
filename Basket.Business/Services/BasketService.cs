using Basket.DataAccess.Models;
using Basket.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Basket.Common.Exceptions;
using Basket.Common.Surrogates;
using System.Linq;
using Basket.Business.MockExternalServices;

namespace Basket.Business.Services
{
    public class BasketService : IBasketService
    {
        readonly IBasketRepository _repository;
        readonly IMockStockService _stockService;

        public BasketService(IBasketRepository repository, IMockStockService stockService)
        {
            _repository = repository;
            _stockService = stockService;
        }
        public async Task<ShoppingCart> AddToBasket(AddProductDto productInfo)
        {
            var existingShoppingCart = await _repository.GetShoppingCart(productInfo.UserId);

            int totalQuantity = GetTotalProductQuantity(productInfo, existingShoppingCart);

            var isStockAvailable = _stockService.IsStockAvailable(productInfo.ProductId, totalQuantity);

            if (!isStockAvailable)
            {
                throw new BusinessException("There is no enough stock!");
            }

            ShoppingCart preparedShoppingCart = PrepareAndGetNewShoppingCart(productInfo, existingShoppingCart);

            await _repository.UpdateShoppingCart(preparedShoppingCart);

            return preparedShoppingCart;
        }

        public async Task<ShoppingCart> GetBasket(int userId)
        {
            return await _repository.GetShoppingCart(userId);
        }

        private int GetTotalProductQuantity(AddProductDto productInfo, ShoppingCart existingShoppingCart)
        {
            if (existingShoppingCart == null)
            {
                return productInfo.Quantity;
            }

            var existingQuantity = existingShoppingCart.Items
                .FirstOrDefault(i => i.ProductId == productInfo.ProductId)?.Quantity;

            if (existingQuantity == null)
            {
                return productInfo.Quantity;
            }

            return (int)existingQuantity + productInfo.Quantity;
        }

        private ShoppingCart PrepareAndGetNewShoppingCart(AddProductDto productInfo, ShoppingCart existingShoppingCart)
        {
            if (existingShoppingCart == null)
            {
                var preparedShoppingCart = new ShoppingCart
                {
                    UserId = productInfo.UserId,
                    Items = new List<ShoppingCartItem>
                    {
                        new ShoppingCartItem
                        {
                            ProductId = productInfo.ProductId,
                            ProductName = productInfo.ProductName,
                            Quantity = productInfo.Quantity
                        }
                    }
                };

                return preparedShoppingCart;
            }

            var existingProduct = existingShoppingCart.Items
                .FirstOrDefault(i => i.ProductId == productInfo.ProductId);

            if (existingProduct != null)
            {
                existingProduct.Quantity += productInfo.Quantity;

                return existingShoppingCart;
            }

            existingShoppingCart.Items.Add(new ShoppingCartItem
            {
                ProductId = productInfo.ProductId,
                ProductName = productInfo.ProductName,
                Quantity = productInfo.Quantity
            });


            return existingShoppingCart;
        }
    }
}

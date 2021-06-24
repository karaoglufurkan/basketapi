using Basket.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Basket.Business.MockExternalServices
{
    public class MockStockService : IMockStockService
    {
        public Dictionary<int,int> ProductIdQuantityMapping { get; set; }

        public MockStockService()
        {
            ProductIdQuantityMapping = new Dictionary<int, int>
            {
                { 1, 5 },
                { 2, 3 },
                { 3, 0 },
                { 4, 10 },
                { 5, 1 }
            };
        }

        public bool IsStockAvailable(int productId, int quantity)
        {
            var isProductAvailable = ProductIdQuantityMapping.ContainsKey(productId);

            if (!isProductAvailable)
            {
                throw new BusinessException("Product not found!");
            }

            var stockQuantity = ProductIdQuantityMapping[productId];

            if (stockQuantity < quantity)
            {
                return false;
            }

            return true;
        }
    }
}

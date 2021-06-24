using System;
using System.Collections.Generic;
using System.Text;

namespace Basket.Business.MockExternalServices
{
    public interface IMockStockService
    {
        bool IsStockAvailable(int productId, int quantity);
    }
}

using System;
using System.Collections.Generic;
using Domain.Products;

namespace Messages
{
    public class IAmBasketResult
    {
        Guid CustomerId { get; }
        IList<Product> Products { get; }
    }
}
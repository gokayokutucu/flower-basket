using System;
using System.Collections.Generic;
using Domain.Products;

namespace Domain.Customer
{
    public class ICustomerBasket
    {
        Guid CustomerId { get; }
        IList<Product> Products { get; }
    }
}
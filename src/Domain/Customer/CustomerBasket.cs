using System;
using System.Collections.Generic;
using Domain.Products;

namespace Domain.Customer
{
    public class CustomerBasket : ICustomerBasket
    {
        public Guid CustomerId { get; set; }
        public IList<Product> Products { get; set; }
    }
}
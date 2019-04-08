using System;
using System.Collections.Generic;
using Domain.Enums;
using Domain.Products;

namespace Messages {
    public interface IAmSubmitBasket {
        Guid CustomerId { get; }
        IList<Product> Products { get; }
    }
}
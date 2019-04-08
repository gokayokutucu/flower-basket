using System.Collections.Generic;
using Domain.Order;
using Domain.Products;

namespace Messages
{
    public interface IOrderProductRequested
    {
        IOrder Order{get;}
        IList<IProduct> Products { get; }
        IList<IOrderProduct> OrderProducts { get; }
    }
}
using System.Collections;
using System.Collections.Generic;
using Domain.Order;
using Domain.Products;

namespace Messages
{
    public interface IOrderAccepted
    {
        IOrder Order { get; }
        IList<IOrderProduct> OrderProducts {get;}
    }
}
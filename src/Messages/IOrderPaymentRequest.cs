using System.Collections.Generic;
using Domain.Order;

namespace Messages
{
    public interface IOrderPaymentRequest
    {
        IOrder Order {get;}
        IList<IOrderProduct> OrderProducts {get;}
    }
}
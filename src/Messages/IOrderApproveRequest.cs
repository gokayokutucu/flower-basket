using System.Collections.Generic;
using Domain.Order;
using Domain.Products;

namespace Messages
{
    public interface IOrderApproveRequest
    {
        IOrder Order{get;}
    }
}
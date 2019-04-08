using System;

namespace Domain.Order
{
    public interface IOrderProduct
    {
        Guid OrderProductId { get; }
        Guid OrderId{get;}
        Guid ProductId{get;}
        string ProductName {get;}
        int Quantity {get;}
        decimal TotalPrice{get;}
        bool IsDeleted{get;}
    }
}
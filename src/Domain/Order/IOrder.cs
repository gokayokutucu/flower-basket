using System;
using Domain.Enums;

namespace Domain.Order {
    public interface IOrder {
        Guid OrderId { get; }
        decimal ItemTotalPrice { get; }
        StatusCode StatusCode { get; }
        Guid CreatedBy { get; } //CustomerID
        DateTime CreatedDate { get; }
        Guid ModifiedBy { get; } //CustomerID
        DateTime ModifiedDate { get; }
        bool IsDeleted { get; }
    }
}
using System;
using Domain.Enums;

namespace Domain.Order
 {
    public class Order : IOrder {
        public Order() : this(Guid.NewGuid()){}
        public Order(Guid orderId)
        {
            OrderId = orderId;
        }
        public Guid OrderId { get; private set; }
        public decimal ItemTotalPrice { get; set; }
        public StatusCode StatusCode { get; set; }
        public Guid CreatedBy { get; set; } //CustomerID
        public DateTime CreatedDate { get; set; }
        public Guid ModifiedBy { get; set; } //CustomerID
        public DateTime ModifiedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
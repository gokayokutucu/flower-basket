using System;

namespace Domain.Order {
    public class OrderProduct : IOrderProduct {
        public OrderProduct () : this (Guid.NewGuid ()) { }
        public OrderProduct (Guid orderProductId) {
            OrderProductId = orderProductId;
        }
        public Guid OrderProductId { get; private set; }
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public bool IsDeleted { get; set; }
    }
}
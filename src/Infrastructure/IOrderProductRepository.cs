using System;

namespace Infrastructure {
    public interface IOrderProductRepository<T> : IGenericRepository<T> where T : class
    {
        void RemoveByOrderId (Guid orderId);
    }
}
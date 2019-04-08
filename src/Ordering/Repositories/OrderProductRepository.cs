using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Order;
using Infrastructure;
using Messages;

namespace Ordering.Repositories
{
    public class OrderProductContext : IContext<OrderProduct>{
        private static IList<OrderProduct> OrderProductList = new List<OrderProduct>();

        public IList<OrderProduct> GetAll(){
            return OrderProductList;
        }        
        public void AddOrUpdate(OrderProduct orderProduct){
            if(orderProduct==null)
                throw new Exception("Order Product object cannot pass as null");
                
            var item = OrderProductList.FirstOrDefault(x=> x.OrderId == orderProduct.OrderId && x.ProductId == orderProduct.ProductId);
            if(item == null){
                OrderProductList.Add(orderProduct);
                return;
            }
            item.ProductName = orderProduct.ProductName;
            item.Quantity = orderProduct.Quantity;
            item.TotalPrice = orderProduct.TotalPrice;
        }

        public OrderProduct GetByID(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Remove(OrderProduct item)
        {
            throw new NotImplementedException();
        }

        public bool Save()
        {
            return true;
        }

        public void AddOrUpdateAll(IList<OrderProduct> items)
        {
            foreach (var item in items)
            {
                AddOrUpdate(item);
            }
        }

        public void RemoveAll(IList<OrderProduct> items)
        {
            throw new NotImplementedException();
        }
    }
    public class OrderProductRepository : IOrderProductRepository<OrderProduct>
    {
        private readonly OrderProductContext _context;
        public OrderProductRepository()
        {
            _context = new OrderProductContext();
        }

        public IList<OrderProduct> GetAll(){
            return _context.GetAll();
        }

        public void AddOrUpdate(OrderProduct orderProduct){
            _context.AddOrUpdate(orderProduct);
        }

        public void AddOrUpdateAll(IList<OrderProduct> orderProducts)
        {
            _context.AddOrUpdateAll(orderProducts);
        }

        public void RemoveAll(IList<OrderProduct> orderProducts)
        {
            var ids = orderProducts.Select(x=> x.OrderId).ToList<Guid>();
            
            var list = _context.GetAll ().Where(x=> ids.Contains(x.OrderId)).ToList();

            foreach (var item in list ) { item.IsDeleted = true; }
        }

        public void RemoveByOrderId(Guid orderId){
            var list = _context.GetAll ().Where(x=> x.OrderId == orderId).ToList();

            foreach (var item in list ) { item.IsDeleted = true; }
        }

        public bool Save(){
            return _context.Save();
        }

        public OrderProduct GetByID(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Remove(OrderProduct item)
        {
            throw new NotImplementedException();
        }
    }
}
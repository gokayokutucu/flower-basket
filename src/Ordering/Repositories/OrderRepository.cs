using System;
using System.Collections.Generic;
using System.Linq;
using Messages;
using Domain.Order;
using Infrastructure;

namespace Ordering.Repositories
{
    public class OrderContext: IContext<Order>{
        private static IList<Order> OrderList = new List<Order>();

        public void AddOrUpdate(Order order){
            var item = OrderList.FirstOrDefault(x=> x.OrderId == order.OrderId);
            if(item == null){
                OrderList.Add(order);
                return;
            }
            item.ItemTotalPrice = order.ItemTotalPrice;
            item.StatusCode = order.StatusCode;
            item.CreatedBy = order.CreatedBy;
            item.CreatedDate = order.CreatedDate;
        }

        public void AddOrUpdateAll(IList<Order> items)
        {
            foreach (var item in items)
            {
                AddOrUpdate(item);
            }
        }

        public IList<Order> GetAll()
        {
            return OrderList;
        }

        public Order GetByID(Guid id)
        {
            return OrderList.FirstOrDefault(x=> x.OrderId == id);
        }

        public void Remove(Order item)
        {
            throw new NotImplementedException();
        }

        public void RemoveAll(IList<Order> items)
        {
            throw new NotImplementedException();
        }

        public bool Save()
        {
            return true;
        }
    }
    public class OrderRepository
    {
        private readonly OrderContext _context;
        public OrderRepository()
        {
            _context = new OrderContext();
        }

        public Order GetByID(Guid id)
        {
            if(id != Guid.Empty)
                return _context.GetByID(id);
            return default(Order);
        }
        public IList<Order> GetAll()
        {
            return _context.GetAll();
        }
        public void AddOrUpdate(Order order){
            _context.AddOrUpdate(order);
        }

         public bool Save()
        {
            return _context.Save();
        }
    }
}
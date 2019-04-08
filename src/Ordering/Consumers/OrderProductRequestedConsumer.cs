using System;
using System.Linq;
using System.Threading.Tasks;
using Domain.Enums;
using Domain.Order;
using MassTransit;
using Messages;
using Ordering.Repositories;

namespace Ordering.Consumers {
    public class OrderProductRequestedConsumer : IConsumer<IOrderProductRequested> {
        private readonly OrderRepository _orderRepository;
        private readonly OrderProductRepository _orderProductRepository;

        public OrderProductRequestedConsumer () { }

        public OrderProductRequestedConsumer (OrderRepository orderRepository, OrderProductRepository orderProductRepository) {
            _orderRepository = orderRepository;
            _orderProductRepository = orderProductRepository;
        }

        public async Task Consume (ConsumeContext<IOrderProductRequested> context) {

            if(context.Message.Products == null)
                throw new Exception("Product cannot be null");

            Order order = (Order)context.Message.Order;
       
            if(context.Message.Order!=null)
            {
                order = _orderRepository.GetByID (context.Message.Order.OrderId);
                _orderProductRepository.RemoveByOrderId (context.Message.Order.OrderId);
            }

            order = new Order () {
                StatusCode = StatusCode.Waiting,
                IsDeleted = false,
                CreatedDate = DateTime.UtcNow,
                ItemTotalPrice = context.Message.Products.Sum (l => l.PricePerUnit)
            };

            _orderRepository.AddOrUpdate (order);

            var orderProductList = context.Message.Products.GroupBy (l => l.ProductId)
                .Select (cl => new OrderProduct {
                    OrderId = order.OrderId,
                        ProductId = cl.First ().ProductId,
                        ProductName = cl.First().Name,
                        Quantity = cl.Count (),
                        TotalPrice = cl.Sum (c => c.PricePerUnit),
                        IsDeleted = false
                }).ToList ();

            _orderProductRepository.AddOrUpdateAll (orderProductList);

            _orderProductRepository.Save ();

            await context.Publish<IOrderApproveRequest>(new {Order = order});

            Console.WriteLine("Order requested successfully.");
            //return Task.CompletedTask;
        }
    }
}
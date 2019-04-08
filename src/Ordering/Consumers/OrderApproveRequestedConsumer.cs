using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Order;
using MassTransit;
using Messages;
using Ordering.Repositories;

namespace Ordering.Consumers {
    public class OrderApproveRequestedConsumer : IConsumer<IOrderApproveRequest> {
        private readonly ProductRepository _productRepository;
        private readonly OrderProductRepository _orderProductRepository;
        private static IList<OrderProduct> OrderProducts;
        public OrderApproveRequestedConsumer () { }

        public OrderApproveRequestedConsumer (ProductRepository productRepository, OrderProductRepository orderProductRepository) {
            _productRepository = productRepository;
            _orderProductRepository = orderProductRepository;
        }
        public async Task Consume (ConsumeContext<IOrderApproveRequest> context) {
            OrderProducts = _orderProductRepository.GetAll().Where(x=> x.OrderId == context.Message.Order.OrderId).ToList();
            if (StockControl (context.Message)) {
                await context.Publish<IOrderPaymentRequest> (new { Order = context.Message.Order, OrderProducts =  OrderProducts});
                Console.WriteLine($"OrderID: {context.Message.Order.OrderId} -> Order Status: {context.Message.Order.StatusCode.ToString()} | Total Price: {context.Message.Order.ItemTotalPrice:C}");
                Console.WriteLine("Order approved successfully.");
            } else {
                Console.WriteLine("Order failed.");
                throw new Exception ("Order Failed");
            }
        }

        private bool StockControl (IOrderApproveRequest contextMessage) {
            foreach (var orderProduct in OrderProducts) {               
                var product = _productRepository.GetAll().FirstOrDefault (p => p.ProductId == orderProduct.ProductId);
                if (orderProduct.Quantity > product.InStock)
                    return false;
            }
            return true;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Enums;
using Domain.Order;
using Domain.Products;
using MassTransit;
using Messages;
using Ordering.Repositories;

namespace Ordering.Consumers {
    public class OrderAcceptedConsumer : IConsumer<IOrderAccepted> {
        private readonly ProductRepository _productRepository;
        private readonly OrderRepository _orderRepository;
        private readonly OrderProductRepository _orderProductRepository;

        public OrderAcceptedConsumer () { }

        public OrderAcceptedConsumer (ProductRepository productRepository, OrderRepository orderRepository, OrderProductRepository orderProductRepository) {
            _productRepository = productRepository;
            _orderRepository = orderRepository;
            _orderProductRepository = orderProductRepository;
        }
        public Task Consume (ConsumeContext<IOrderAccepted> context) {
            //Decrease or increase quantity of product stock numbers randomly
            RandomStockChange();

            Order order = _orderRepository.GetByID (context.Message.Order.OrderId);
            if (order == null){
                Console.WriteLine("Order failed.");
                throw new Exception ("Order Failed");
            }

            order.StatusCode = StatusCode.Accepted;
            order.IsDeleted = false;
            order.ModifiedDate = DateTime.UtcNow;

            _orderRepository.AddOrUpdate (order);

            var products = new List<Product>();

            foreach (var orderProduct in context.Message.OrderProducts.Where(x=> x.OrderId == order.OrderId))
            {
                var product = _productRepository.GetAll().FirstOrDefault(x=> x.ProductId == orderProduct.ProductId);
                //Decrease stock number
                product.InStock = product.InStock - orderProduct.Quantity;
                products.Add(product);
            }

            _productRepository.AddOrUpdateAll(products);

            _productRepository.Save();
            _orderRepository.Save();

            Console.WriteLine("Order accepted.");
            Console.WriteLine($"OrderID: {order.OrderId} -> Order Status: {order.StatusCode.ToString()} | Total Price: {order.ItemTotalPrice:C}");

            return Task.CompletedTask;
        }

        private void RandomStockChange(){
            var list = new List<Product>();
            foreach (var product in _productRepository.GetAll())
            {
                product.InStock = Random.Next(1, 10);
                list.Add(product);
            }
            _productRepository.AddOrUpdateAll(list);
        }

        private static readonly Random Random = new Random();
    }
}
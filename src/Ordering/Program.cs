using System;
using System.Linq;
using MassTransit;
using Messages;
using Ordering.Consumers;
using Ordering.Repositories;

namespace Ordering {
    class Program {
        static void Main (string[] args) {
            var orderRepo = new OrderRepository ();
            var productRepo = new ProductRepository ();
            var orderProductRepo = new OrderProductRepository ();
            var bus = Bus.Factory.CreateUsingRabbitMq (sbc => {
                var host = sbc.Host (new Uri ("rabbitmq://localhost"), h => {
                    h.Username ("guest");
                    h.Password ("guest");
                });

                sbc.ReceiveEndpoint (host, "Ordering", ep => {
                    ep.Consumer (() => new OrderProductRequestedConsumer (orderRepo, orderProductRepo));
                    ep.Consumer (() => new OrderAcceptedConsumer (productRepo, orderRepo, orderProductRepo));
                    ep.Consumer (() => new OrderApproveRequestedConsumer (productRepo, orderProductRepo));
                });
            });

            bus.Start ();

            Console.WriteLine ("Welcome to Order Page");
            Console.WriteLine ("Press Q key to exit");
            Console.WriteLine ("Press E key to make payment");

            for (;;) {
                var consoleKeyInfo = Console.ReadKey (true);
                if (consoleKeyInfo.Key == ConsoleKey.Q) break;
                if (orderRepo.GetAll ().Count > 0) {
                    Console.WriteLine ("-- Order --");
                    Console.WriteLine (string.Join (Environment.NewLine, orderRepo.GetAll ().Select (x => $"OrderID: {x.OrderId} -> Order Status: {x.StatusCode.ToString()} | Total Price: {x.ItemTotalPrice:C}").First ()));
                }
            }

            bus.Stop ();
        }
    }
}
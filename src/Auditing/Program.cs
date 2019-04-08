using System;
using System.Linq;
using MassTransit;

namespace Auditing {
    class Program {
        static void Main (string[] args) {
            var bus = Bus.Factory.CreateUsingRabbitMq (sbc => {
                var host = sbc.Host (new Uri ("rabbitmq://localhost"), h => {
                    h.Username ("guest");
                    h.Password ("guest");
                });

                sbc.ReceiveEndpoint (host, "Auditing", ep => {
                    ep.Consumer (() => new OrderRequestedConsumer ());
                    ep.Consumer (() => new OrderAcceptedConsumer ());
                });
            });

            bus.Start ();

            Console.WriteLine ("Welcome to Auditing");
            Console.WriteLine ("Press Q key to exit");

            for (;;) {
                var consoleKeyInfo = Console.ReadKey (true);
                if (consoleKeyInfo.Key == ConsoleKey.Q) break;

                Console.WriteLine ("-- Logging is running --");
            }

            bus.Stop ();
        }
    }
}
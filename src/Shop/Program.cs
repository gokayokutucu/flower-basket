using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Customer;
using Domain.Enums;
using Domain.Products;
using MassTransit;
using Messages;

namespace Shop
{
    class Program
    {
        private static readonly Guid CustomerId = new Guid("27900000-0036-4a00-af3a-08d6978b077c");

        static void Main(string[] args)
        {
            MainAsync(args).GetAwaiter().GetResult();
        }

        static async Task MainAsync(string[] args)
        {
            var bus = Bus.Factory.CreateUsingRabbitMq(sbc =>
            {
                var host = sbc.Host(new Uri("rabbitmq://localhost"), h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });

                sbc.ReceiveEndpoint(host, "FlowerShop", ep =>
                {
                    ep.Consumer(() => new OrderAcceptedRequestedFaultConsumer());
                    ep.Consumer(() => new OrderApproveRequestedFaultConsumer());
                    ep.Consumer(() => new SubmitBasketConsumer());
                });
            });

            bus.Start();

            Console.WriteLine("Welcome to the Flower Shop");
            Console.WriteLine("Press Q key to exit");
            Console.WriteLine("Press A key to activate Basket Adding Mode - (Default Mode)");
            Console.WriteLine("Press R key to activate Basket Removing Mode");
            Console.WriteLine($"Press [0..9] key to order some products");
            Console.WriteLine(string.Join(Environment.NewLine,
                ProductList.Select((x, i) => $"[{i}]: {x.Name} @ {x.PricePerUnit:C} X {x.InStock}")));

            var products = new List<Product>();
            BasketMode basketMode = BasketMode.Adding;
            while (true)
            {
                var consoleKeyInfo = Console.ReadKey(true);
                if (consoleKeyInfo.Key == ConsoleKey.Q)
                {
                    break;
                }

                if (consoleKeyInfo.Key == ConsoleKey.A)
                {
                    basketMode = BasketMode.Adding;
                    Console.WriteLine("Basket Adding Mode activated");
                }

                if (consoleKeyInfo.Key == ConsoleKey.R)
                {
                    basketMode = BasketMode.Removing;
                    Console.WriteLine("Basket Removing Mode activated");
                }

                if (char.IsNumber(consoleKeyInfo.KeyChar))
                {
                    int index = (int) char.GetNumericValue(consoleKeyInfo.KeyChar);

                    //Product stock check for adding basket. It's a basic client-check
                    var product = ProductList[index];
                    if (basketMode == BasketMode.Adding && product.InStock > 0)
                    {
                        product.InStock--;
                        products.Add(product);
                        await SendBasket(bus, products);
                        Console.WriteLine($"{product.Name} is added to basket");
                    }
                    else if (basketMode == BasketMode.Adding && product.InStock == 0)
                    {
                        Console.WriteLine($"{product.Name} product is not in stock");
                    }
                    else if (basketMode == BasketMode.Removing &&
                             products.Count(x => x.ProductId == product.ProductId) > 0)
                    {
                        product.InStock++;
                        products.Remove(product);
                        await SendBasket(bus, products);
                        Console.WriteLine($"{product.Name} is removed from basket");
                    }
                    else if (basketMode == BasketMode.Removing &&
                             products.Count(x => x.ProductId == product.ProductId) == 0)
                    {
                        Console.WriteLine($"{product.Name} is not in the basket");
                    }
                }

                if (consoleKeyInfo.Key == ConsoleKey.Enter && products.Count > 0)
                {
                    await bus.Publish<IOrderProductRequested>(new
                    {
                        Products = products
                    });

                    Console.WriteLine("Redirecting to Order Page");

                    products.Clear();
                }
            }

            bus.Stop();
        }

        private static async Task SendBasket(IBusControl bus, IList<Product> products)
        {
            var uri = new Uri("rabbitmq://localhost/FlowerShop");
            var endpoint = await bus.GetSendEndpoint(uri);
            await endpoint.Send<IAmSubmitBasket>(new
            {
                CustomerId = CustomerId,
                Products = products,
            });
        }

        private static IList<Product> ProductList = new List<Product>
        {
            new Product()
            {
                ProductId = new Guid("a84d8438-296b-48c3-9658-9bd34a119cd2"),
                Name = "Papatya",
                PricePerUnit = 1.5M,
                InStock = 8
            },
            new Product()
            {
                ProductId = new Guid("e1675698-7588-4d13-9537-bbb30e026d96"),
                Name = "Yasemin",
                PricePerUnit = 2M,
                InStock = 4
            },
            new Product()
            {
                ProductId = new Guid("3acf8f5a-7cc1-465c-8b9f-f7028659728c"),
                Name = "Orkide",
                PricePerUnit = 5M,
                InStock = 4
            },
            new Product()
            {
                ProductId = new Guid("bc51c9a1-dfda-42b3-a918-f06f14556a31"),
                Name = "Lilyum",
                PricePerUnit = 4.5M,
                InStock = 2
            },
            new Product()
            {
                ProductId = new Guid("183cf819-1bb1-4bcb-8cef-3c103c956bba"),
                Name = "Manolya",
                PricePerUnit = 1M,
                InStock = 8
            },
            new Product()
            {
                ProductId = new Guid("22a12037-9c0c-4784-ba0e-e02030a2f614"),
                Name = "Ferforje",
                PricePerUnit = 3.5M,
                InStock = 3
            },
            new Product()
            {
                ProductId = new Guid("5b48fbfc-184d-4180-bfae-46321a680715"),
                Name = "Teraryum",
                PricePerUnit = 4.5M,
                InStock = 1
            },
            new Product()
            {
                ProductId = new Guid("c4c3edb7-a943-411c-b67d-d2e630c6dfd7"),
                Name = "Kaktüs",
                PricePerUnit = 0.5M,
                InStock = 10
            },
            new Product()
            {
                ProductId = new Guid("64c01c41-a900-425c-b17c-4c7ff920cc06"),
                Name = "Sukulent",
                PricePerUnit = 5M,
                InStock = 2
            },
            new Product()
            {
                ProductId = new Guid("62b94728-f8c9-4769-b9f8-6e2abf52bf13"),
                Name = "Çikolata",
                PricePerUnit = 4M,
                InStock = 9
            }
        };
    }
}
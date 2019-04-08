using System;
using System.Threading.Tasks;
using MassTransit;
using Messages;

namespace Shop
{
    public class OrderAcceptedRequestedFaultConsumer : IConsumer<Fault<IOrderAccepted>>
    {
        public Task Consume(ConsumeContext<Fault<IOrderAccepted>> context)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("The following order was not accepted:");
            Console.ResetColor();
            return Task.CompletedTask;
        }
    }
}
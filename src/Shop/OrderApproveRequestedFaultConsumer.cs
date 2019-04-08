using System;
using System.Threading.Tasks;
using MassTransit;
using Messages;

namespace Shop
{
    public class OrderApproveRequestedFaultConsumer : IConsumer<Fault<IOrderApproveRequest>>
    {
        public Task Consume(ConsumeContext<Fault<IOrderApproveRequest>> context)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("The following order was not processed:");
            Console.ResetColor();
            return Task.CompletedTask;
        }
    }
}
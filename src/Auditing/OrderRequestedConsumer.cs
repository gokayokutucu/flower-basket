using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Messages;
using Domain.Products;

namespace Auditing
{
    public class OrderRequestedConsumer : IConsumer<IOrderRequested>
    {     
        public Task Consume(ConsumeContext<IOrderRequested> context)
        {
            //do something

            return Task.CompletedTask;
        }
    }
}
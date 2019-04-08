using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Messages;
using Domain.Products;

namespace Auditing
{
    public class OrderAcceptedConsumer : IConsumer<IOrderAccepted>
    {
        public Task Consume(ConsumeContext<IOrderAccepted> context)
        {
            //Do something to report
            return Task.CompletedTask;
        }
    }
}
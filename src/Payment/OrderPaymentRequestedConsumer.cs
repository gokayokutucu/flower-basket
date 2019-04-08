using System;
using System.Threading.Tasks;
using MassTransit;
using Messages;

namespace Payments
{
    public class OrderPaymentRequestedConsumer : IConsumer<IOrderPaymentRequest>
    {
        public async Task Consume(ConsumeContext<IOrderPaymentRequest> context)
        {
            Console.WriteLine("Payment consumed");
            if (Checkout(context.Message))
            {
                await context.Publish<IOrderAccepted>(new {Order = context.Message.Order, OrderProducts = context.Message.OrderProducts});
            }
            else
            {
                Console.WriteLine("Payment Failed Exception");
                //throw new Exception("Payment Failed");
            }
        }

        private bool Checkout(IOrderPaymentRequest contextMessage)
        {
            // 1 in 10 payments fail
            var paymentFailed = Random.Next(1, 10) == 1;

            if(paymentFailed) Console.WriteLine("Payment Failed");
            if(!paymentFailed) Console.WriteLine("Payment Successful");

            return !paymentFailed;
        }

        private static readonly Random Random = new Random();
    }
}
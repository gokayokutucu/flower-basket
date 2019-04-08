using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Domain.Customer;
using MassTransit;
using Messages;
using Newtonsoft.Json;

namespace Shop
{
    public class SubmitBasketConsumer : IConsumer<IAmSubmitBasket>
    {
        private readonly HttpClient _client = new HttpClient();
       
        public async Task Consume(ConsumeContext<IAmSubmitBasket> context)
        {
            _client.BaseAddress = new Uri("http://localhost:5000/");
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add( new MediaTypeWithQualityHeaderValue("application/json"));
            
            var response = await CreateCustomerProductAsync(new CustomerBasket()
            {
                CustomerId = context.Message.CustomerId,
                Products = context.Message.Products,
            });
            if (!response)
                throw new InvalidOperationException("Basket not found");

        }
        
        private async Task<bool> CreateCustomerProductAsync(CustomerBasket customerBasket)
        {
            var response = await _client.PostAsync(
                "api/Basket",  
                 new StringContent(JsonConvert.SerializeObject(customerBasket), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            
            var stringResult = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<bool>(stringResult);
        }
    }
}
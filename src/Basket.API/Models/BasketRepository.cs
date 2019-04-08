using System;
using System.Threading.Tasks;
using Domain.Customer;

namespace Basket.API.Models
{
    //We can use it for persistent database
    public class BasketRepository : IBasketRepository
    {
        public Task<bool> AddBasketAsync(CustomerBasket basket)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> DeleteBasketAsync(Guid productId, Guid customerId)
        {
            throw new System.NotImplementedException();
        }

        public Task<CustomerBasket> GetBasketAsync(Guid customerId)
        {
            throw new System.NotImplementedException();
        }
    }
}
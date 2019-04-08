using System;
using System.Threading.Tasks;
using Domain.Customer;

namespace Basket.API.Models
{
    public interface IBasketRepository
    {
        Task<CustomerBasket> GetBasketAsync(Guid customerId);
        Task<bool> AddBasketAsync(CustomerBasket basket);
        Task<bool> DeleteBasketAsync(Guid productId, Guid customerId);
    }
}
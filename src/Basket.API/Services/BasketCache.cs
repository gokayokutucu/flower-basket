using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Customer;
using Domain.Products;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Basket.API.Services
{
    public class BasketCache
    {
        private readonly IDistributedCache _cache;

        public BasketCache(IDistributedCache cache)
        {
            _cache = cache;
        }

        public CustomerBasket GetBasket(string customerId)
        {
            return GetCachedObject<CustomerBasket> ($"customerBasket-{customerId}");
        }
        
        public bool SetBasketToCache(CustomerBasket customerBasket)
        {
            SetCachedObject($"customerBasket-{customerBasket.CustomerId.ToString()}", customerBasket);
            return true;
        }

        private T GetCachedObject<T>(string cacheKey)
        {
            if (!string.IsNullOrEmpty(cacheKey))
            {
                // Get the cached item
                string cachedObjectJson = _cache.GetString(cacheKey);

                // If there was a cached item then deserialise this 
                if (!string.IsNullOrEmpty(cachedObjectJson))
                {
                    try
                    {
                        T cachedObject = JsonConvert.DeserializeObject<T>(cachedObjectJson);
                        return cachedObject;
                    }
                    catch (Exception e)
                    {
                        return default(T);
                    }
                }
            }

            return default(T);
        }

        private bool SetCachedObject(string cacheKey, dynamic objectToCache)
        {
            if (string.IsNullOrEmpty(cacheKey ) || objectToCache == null)
            {
                return false;
            }

            try
            {
                string serializedObjectToCache = JsonConvert.SerializeObject(objectToCache);
                _cache.SetString(cacheKey, serializedObjectToCache,
                    new DistributedCacheEntryOptions() {AbsoluteExpiration = DateTime.Now.AddMinutes(30)});
            }
            catch (Exception e)
            {
                return default(bool);
            }

            return true;
        }
    }
}
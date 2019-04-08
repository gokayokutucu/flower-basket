using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Basket.API.Services;
using Domain.Customer;
using Domain.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Basket.API.Controllers {
    [Route ("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase {
        //private readonly string _connectionString;
        private readonly BasketCache _cache;
        public BasketController (IConfiguration configuration, BasketCache cache) {
           // _connectionString = configuration.GetConnectionString ("DefaultConnection");
            _cache = cache;
        }
        // GET api/basket
        [HttpGet ("{customerId}")]
        [ProducesResponseType(typeof(CustomerBasket), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Get (Guid customerId) {
            var basketCustomer = _cache.GetBasket (customerId.ToString());

            // If no basketCustomer was found, then return a 404
            if (basketCustomer == null)
                return NotFound ();
            return Ok (basketCustomer);
        }

        // POST api/basket
        [HttpPost]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status304NotModified)]
        public ActionResult Post ([FromBody] CustomerBasket customerBasket) 
        { 
            
            if (customerBasket == null) {
                return StatusCode ((int) HttpStatusCode.NotModified);
            }

            bool isModified =  _cache.SetBasketToCache(customerBasket);
            
            if (!isModified)
                return StatusCode ((int) HttpStatusCode.NotModified);
            
            return Ok (true);
        }

        // PUT api/basket/5
        [HttpPut ("{id}")]
        public void Put (int id, [FromBody] string value) { }

        // DELETE api/basket
        [HttpDelete]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status304NotModified)]
        public void Delete([FromBody] CustomerBasket customerBasket)
        {
        }
    }
}
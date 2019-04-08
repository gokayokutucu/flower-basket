using System;

namespace Domain.Products
{
    public class Product : IProduct
    {
        public Guid ProductId { get; set; } //private set on deployment. I've removed private to test api
        public string Name { get; set; }
        public decimal PricePerUnit { get; set; }
        public int InStock { get; set; }
    }
}
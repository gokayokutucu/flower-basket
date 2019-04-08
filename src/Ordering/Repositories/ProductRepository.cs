using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Products;
using Infrastructure;

namespace Ordering.Repositories
{
    public class ProductContext : IContext<Product>{
        private static IList<Product> ProductList = new List<Product>
        {
           new Product()
            {
                ProductId = new Guid("a84d8438-296b-48c3-9658-9bd34a119cd2"),
                Name = "Papatya",
                PricePerUnit = 1.5M,
                InStock = 8
            },
            new Product()
            {
                ProductId = new Guid("e1675698-7588-4d13-9537-bbb30e026d96"),
                Name = "Yasemin",
                PricePerUnit = 2M,
                InStock = 4
            },
            new Product()
            {
                ProductId = new Guid("3acf8f5a-7cc1-465c-8b9f-f7028659728c"),
                Name = "Orkide",
                PricePerUnit = 5M,
                InStock = 4
            },
            new Product()
            {
                ProductId = new Guid("bc51c9a1-dfda-42b3-a918-f06f14556a31"),
                Name = "Lilyum",
                PricePerUnit = 4.5M,
                InStock = 2
            },
            new Product()
            {
                ProductId = new Guid("183cf819-1bb1-4bcb-8cef-3c103c956bba"),
                Name = "Manolya",
                PricePerUnit = 1M,
                InStock = 8
            },
            new Product()
            {
                ProductId = new Guid("22a12037-9c0c-4784-ba0e-e02030a2f614"),
                Name = "Ferforje",
                PricePerUnit = 3.5M,
                InStock = 3
            },
            new Product()
            {
                ProductId = new Guid("5b48fbfc-184d-4180-bfae-46321a680715"),
                Name = "Teraryum",
                PricePerUnit = 4.5M,
                InStock = 1
            },
            new Product()
            {
                ProductId = new Guid("c4c3edb7-a943-411c-b67d-d2e630c6dfd7"),
                Name = "Kaktüs",
                PricePerUnit = 0.5M,
                InStock = 10
            },
            new Product()
            {
                ProductId = new Guid("64c01c41-a900-425c-b17c-4c7ff920cc06"),
                Name = "Sukulent",
                PricePerUnit = 5M,
                InStock = 2
            },
            new Product()
            {
                ProductId = new Guid("62b94728-f8c9-4769-b9f8-6e2abf52bf13"),
                Name = "Çikolata",
                PricePerUnit = 4M,
                InStock = 9
            }
        };

        public IList<Product> GetAll(){
            return ProductList;
        }

        public void AddOrUpdate(Product product){
            var item = ProductList.FirstOrDefault(x=> x.ProductId == product.ProductId);
            if(item == null){
                ProductList.Add(product);
                return;
            }
            item.Name = product.Name;
            item.InStock = product.InStock;
            item.PricePerUnit = product.PricePerUnit;
        }

        public Product GetByID(Guid id)
        {
            return ProductList.FirstOrDefault(x=> x.ProductId == id);
        }

        public void Remove(Product item)
        {
            throw new NotImplementedException();
        }

        public bool Save()
        {
            return true;
        }

        public void AddOrUpdateAll(IList<Product> items)
        {
            foreach (var item in items)
            {
                AddOrUpdate(item);
            }
        }

        public void RemoveAll(IList<Product> items)
        {
            throw new NotImplementedException();
        }
    }
    public class ProductRepository
    {
        private readonly ProductContext _context;
        public ProductRepository()
        {
            _context = new ProductContext();
        }

        public IList<Product> GetAll(){
            return _context.GetAll();
        }

        public bool Save()
        {
            return true;
        }

        public void AddOrUpdate(Product product){
            _context.AddOrUpdate(product);
        }

        public void AddOrUpdateAll(IList<Product> products){
            _context.AddOrUpdateAll(products);
        }
    }
}
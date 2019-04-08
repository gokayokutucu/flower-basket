using System;

namespace Domain.Products
{
  public interface IProduct
  {
        Guid ProductId {get; }
        string Name { get; }
        decimal PricePerUnit { get; }
        int InStock { get;  }
  }
}
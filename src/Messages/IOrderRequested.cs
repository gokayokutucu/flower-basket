using System.Collections.Generic;
using Domain.Products;

namespace Messages
{
    public interface IOrderRequested
    {
        IList<IProduct> Products { get; }
    }
}

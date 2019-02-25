using System;
using System.Collections.Generic;
using System.Text;

namespace RiceShop.Clb.Interface
{
   public interface IUnitOfWork:IDisposable
    {

        ISupplierRepositotry Supplier { get; }
        IProductRepository Product { get; }
        ICategoryRepository Category { get; }
        IStockRepository Stock { get; }
        int Complete();
    
    }
}

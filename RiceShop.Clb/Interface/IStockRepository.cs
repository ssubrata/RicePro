using RiceShop.Clb.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RiceShop.Clb.Interface
{
    public interface IStockRepository:IRepository<Stock>
    {
        IEnumerable<Stock> GetStockWithProduct();
    }
}

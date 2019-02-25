using Microsoft.EntityFrameworkCore;
using RiceShop.Clb.Entity;
using RiceShop.Clb.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RiceShop.Clb
{
    public class StockRepository : Repository<Stock>, IStockRepository
    {
        public StockRepository(DatadbContext context) : base(context)
        {

        }

        private DatadbContext DatadbContext
        {
            get { return Context as DatadbContext; }
        }

        public IEnumerable<Stock> GetStockWithProduct()
        {
            return DatadbContext.Stocks.Include(f => f.Product).ToList();
        }
    }
}

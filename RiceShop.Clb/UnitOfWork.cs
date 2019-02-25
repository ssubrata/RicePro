using RiceShop.Clb.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace RiceShop.Clb
{
    public class UnitOfWork : IUnitOfWork
    {
        public ISupplierRepositotry Supplier { get; private set; }
        public IProductRepository Product { get; private set; }
        public ICategoryRepository Category { get; private set; }
        public IStockRepository Stock { get; private set; }

        private readonly DatadbContext _context;
        public UnitOfWork(DatadbContext context)
        {
            _context = context;
            Supplier = new SupplierRepository(_context);
            Product = new ProductRepository(_context);
            Category = new CategoryRepository(_context);
            Stock = new StockRepository(_context);
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}

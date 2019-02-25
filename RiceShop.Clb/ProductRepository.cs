using Microsoft.EntityFrameworkCore;
using RiceShop.Clb.Entity;
using RiceShop.Clb.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RiceShop.Clb
{
  public class ProductRepository:Repository<Product>,IProductRepository
    {
        public ProductRepository(DatadbContext context):base(context){ }

        public IEnumerable<Product> GetProductWithCategory()
        {
            return DatadbContext.Products.Include(f => f.Category).ToList();
        }
        public DatadbContext DatadbContext
        {
            get { return Context as DatadbContext; }
        }
    }
}

﻿using RiceShop.Clb.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RiceShop.Clb.Interface
{
   public interface IProductRepository:IRepository<Product>
    {
        IEnumerable<Product> GetProductWithCategory();
    }
}

using RiceShop.Clb.Entity;
using RiceShop.Clb.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace RiceShop.Clb
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(DatadbContext context) : base(context)
        {

        }

        public DatadbContext DatadbContext
        {
            get
            {
                return Context as DatadbContext;
            }
        }

    }
}

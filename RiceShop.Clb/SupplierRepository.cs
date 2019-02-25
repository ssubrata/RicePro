using RiceShop.Clb.Entity;
using RiceShop.Clb.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace RiceShop.Clb
{
    public class SupplierRepository : Repository<Supplier>, ISupplierRepositotry
    {
        public SupplierRepository(DatadbContext context) : base(context)
        {

        }
        public DatadbContext DatadbContext
        {
            get { return Context as DatadbContext; }
        }
    }

}


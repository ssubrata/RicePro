using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RiceShop.Clb.Entity
{
    public class Supplier
    {
        [Key]
        public int SupplierId { get; set; }
        public string OwnerName { get; set; }
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public string ContactName { get; set; }
        public List<Product> Products { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RiceShop.Clb.Entity
{
    public class Sale
    {
        [Key]
        public int SaleId { get; set; }
        public Order  Order { get; set; }
        public decimal TotalSale { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RiceShop.Clb.Entity
{
    public class Stock
    {
        [Key]
        public int StockId { get; set; }
        public string StockNo { get; set; }
        public decimal UnitPrice { get; set; }
        public double Quantity { get; set; }
        public DateTime StockInDate { get; set; }
        public int  ProductId { get; set; }
        public Product Product { get; set; }
        public Supplier Supplier { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RiceShop.Clb.Entity
{
   public class Invoice
    {
        [Key]
        public int InvocieId { get; set; }
        public int InvoiceNo { get; set; }
        public int OrderId { get; set; }
    }
}

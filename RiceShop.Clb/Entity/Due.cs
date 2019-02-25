using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RiceShop.Clb.Entity
{
    public class Due
    {
        [Key]
        public int DueId { get; set; }
        public decimal TotalDue { get; set; }
        public Order Order { get; set; }
    }
}

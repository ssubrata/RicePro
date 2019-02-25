using System;
using System.Collections.Generic;
using System.Text;

namespace RiceShop.Clb.Entity
{
  public  class Customer
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public List<Order> Orders { get; set; }
    }
}

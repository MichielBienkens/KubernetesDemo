using System;
using System.Collections.Generic;

namespace OrdersAPI.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string ProductIds { get; set; }
        public DateTime OrderDate { get; set; }
        public double TotalPrice { get; set; }
    }
}

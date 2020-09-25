using System.Collections.Generic;

namespace OrdersAPI.Models
{
    public class CreateOrder
    {
        public List<int> ProductIds { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace Kafka.Stock.Models
{
    public class OrderViewModel
    {
        public string Customer { get; set; }
        public string CustomerDocument { get; set; }
        public DateTime CreatedAt { get; set; }
        public IEnumerable<OrderItemViewModel> Items { get; set; }
    }

    public class OrderItemViewModel
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}

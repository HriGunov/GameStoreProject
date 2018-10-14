using System;
using System.Collections.Generic;

namespace GameStore.Data.Models
{
    public class Order
    {
        public int Id { get; set; }

        public int AccountId { get; set; }
        public Account Account { get; set; }

        public ICollection<OrderProducts> OrderProducts { get; set; } = new List<OrderProducts>();

        public DateTime OrderTimestamp { get; set; } = DateTime.Now;
    }
}
using System;
using System.Collections.Generic;

namespace GameStore.Data.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public Account Account { get; set; }

        public ICollection<Product> PurchasedProducts { get; set; }

        public DateTime OrderTimestamp { get; set; }
    }
}
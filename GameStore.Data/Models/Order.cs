using System;
using System.Collections.Generic;

namespace GameStore.Data.Models
{
    public class Order
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public Account Customer { get; set; }
         

        public ICollection<Product> PurchasedProduct { get; set; }

        public DateTime OrderTimestamp { get; set; }
    }
}
using System;

namespace GameStore.Data.Models
{
    public class Order
    {
        public string Id { get; set; }

        public int CustomerId { get; set; }

        public Account Customer { get; set; }

        public int ProductId { get; set; }

        public Product PurchasedProduct { get; set; }

        public DateTime OrderTimestamp { get; set; }
    }
}
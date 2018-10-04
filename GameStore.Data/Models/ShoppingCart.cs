using System.Collections.Generic;

namespace GameStore.Data.Models
{
    public class ShoppingCart
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public Account Customer { get; set; }

        public List<Product> Products { get; set; }
    }
}
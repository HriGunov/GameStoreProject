using System.Collections.Generic;

namespace GameStore.Data.Models
{
    public class ShoppingCart
    {
        public int Id { get; set; }

        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
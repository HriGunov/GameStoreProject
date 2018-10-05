using System.Collections.Generic;

namespace GameStore.Data.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public decimal Price { get; set; }

        public ICollection<Comment> Comments { get; set; }
        public ICollection<Genre> Genre { get; set; }

        public bool IsDeleted { get; set; }
        public bool IsOnSale { get; set; }
    }
}
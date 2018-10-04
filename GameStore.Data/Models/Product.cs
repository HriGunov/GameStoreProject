namespace GameStore.Data.Models
{
    public class Product
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public decimal Price { get; set; }
        public int GenreId { get; set; }
        public Genre Genre { get; set; }

        public bool IsDeleted { get; set; }
        public bool IsOnSale { get; set; }
    }
}
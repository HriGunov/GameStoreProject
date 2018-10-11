namespace GameStore.Data.Models
{
    public class ShoppingCartProducts
    {
        public int ShoppingCartId { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public ShoppingCart ShoppingCart { get; set; }
    }
}
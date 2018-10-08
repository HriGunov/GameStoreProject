using GameStore.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Data.Context.Abstract
{
    public interface IGameStoreContext
    {
        DbSet<Product> Products { get; set; }
        DbSet<Account> Accounts { get; set; }
        DbSet<Genre> Genres { get; set; }
        DbSet<ShoppingCart> ShoppingCarts { get; set; }
        DbSet<Order> Orders { get; set; }
        int SaveChanges();
    }
}
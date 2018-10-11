using GameStore.Data.Context.Abstract;
using GameStore.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Data.Context
{
    public class GameStoreContext : DbContext, IGameStoreContext
    {
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<ShoppingCartProducts> ShoppingCartProducts { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder
                    .UseSqlServer(
                        "Server=tcp:gamestoretp.database.windows.net,1433;Initial Catalog=gamestore;Persist Security Info=False;User ID=gamestoreroot;Password=Root123#$;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ShoppingCartProducts>()
                .HasKey(p => new {p.ProductId, p.ShoppingCartId});

            modelBuilder.Entity<Account>()
                .HasIndex(a => a.Username)
                .IsUnique(true);

            modelBuilder.Entity<Product>()
                .HasIndex(p => p.Name)
                .IsUnique(true);

            base.OnModelCreating(modelBuilder);
        }
    }
}
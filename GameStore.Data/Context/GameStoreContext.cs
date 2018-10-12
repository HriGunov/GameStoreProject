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
            #region ShoppingCartProducts Setup
            modelBuilder.Entity<ShoppingCartProducts>()
                .HasKey(p => new { p.ProductId, p.ShoppingCartId });

            modelBuilder.Entity<ShoppingCartProducts>().Property(s => s.ShoppingCartId)
                        .IsRequired(true);

            modelBuilder.Entity<ShoppingCartProducts>().Property(s => s.ProductId)
                        .IsRequired(true);
            #endregion

            #region Account Setup
            modelBuilder.Entity<Account>()
                .HasIndex(a => a.Username)
                .IsUnique(true);

            modelBuilder.Entity<Account>().Property(x => x.FirstName)
                        .HasMaxLength(20)
                        .IsRequired(true);

            modelBuilder.Entity<Account>().Property(x => x.LastName)
                        .HasMaxLength(20)
                        .IsRequired(true);

            modelBuilder.Entity<Account>().Property(x => x.Username)
                        .HasMaxLength(20)
                        .IsRequired(true);

            modelBuilder.Entity<Account>().Property(x => x.Password)
                        .IsRequired(true);
            #endregion

            #region Comment Setup
            modelBuilder.Entity<Comment>().Property(c => c.AccountId)
                        .IsRequired(true);

            modelBuilder.Entity<Comment>().Property(c => c.ProductId)
                        .IsRequired(true);

            modelBuilder.Entity<Comment>().Property(c => c.Text)
                        .HasMaxLength(100)
                        .IsRequired(true);
            #endregion

            #region Genre Setup
            modelBuilder.Entity<Genre>().Property(g => g.Name)
                        .IsRequired(true);
            #endregion

            #region Order Setup
            modelBuilder.Entity<Order>().Property(o => o.AccountId)
                        .IsRequired(true);
            #endregion

            #region Product Setup
            modelBuilder.Entity<Product>()
                .HasIndex(p => p.Name)
                .IsUnique(true);

            modelBuilder.Entity<Product>().Property(p => p.Name)
                        .HasMaxLength(20)
                        .IsRequired(true);

            modelBuilder.Entity<Product>().Property(p => p.Description)
                        .HasMaxLength(100)
                        .IsRequired(true);

            modelBuilder.Entity<Product>().Property(p => p.Price)
                        .IsRequired(true);
            #endregion

            base.OnModelCreating(modelBuilder);
        }
    }
}
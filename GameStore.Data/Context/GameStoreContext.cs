using System;
using System.Linq;
using GameStore.Data.Context.Configurations;
using GameStore.Data.Models;
using GameStore.Data.Models.Abstract;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Data.Context
{
    public class GameStoreContext : IdentityDbContext<Account>
    {
        public GameStoreContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Comment> Comments { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<ShoppingCartProducts> ShoppingCartProducts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProducts> OrdersProducts { get; set; }

        public override int SaveChanges()
        {
            this.ApplyAuditInfoRules();
            this.ApplyDeletionRules();

            return base.SaveChanges();
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AccountConfiguration());
            modelBuilder.ApplyConfiguration(new CommentConfiguration());
            modelBuilder.ApplyConfiguration(new GenreConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new ShoppingCartProductsConfiguration());
            modelBuilder.ApplyConfiguration(new OrderProductsConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        private void ApplyDeletionRules()
        {
            var entitiesForDeletion = this.ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Deleted && e.Entity is IDeletable);

            foreach (var entry in entitiesForDeletion)
            {
                var entity = (IDeletable)entry.Entity;
                entity.DeletedOn = DateTime.Now;
                entity.IsDeleted = true;
                entry.State = EntityState.Modified;
            }
        }

        private void ApplyAuditInfoRules()
        {
            var newlyCreatedEntities = this.ChangeTracker.Entries()
                .Where(e => e.Entity is IAuditable && ((e.State == EntityState.Added) || (e.State == EntityState.Modified)));

            foreach (var entry in newlyCreatedEntities)
            {
                var entity = (IAuditable) entry.Entity;

                if (entry.State == EntityState.Added && entity.CreatedOn == null)
                {
                    entity.CreatedOn = DateTime.Now;
                }
                else
                {
                    entity.ModifiedOn = DateTime.Now;
                }
            }
        }

    }
}
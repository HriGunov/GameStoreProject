using GameStore.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameStore.Data.Context.Configurations
{
    internal class ShoppingCartProductsConfiguration : IEntityTypeConfiguration<ShoppingCartProducts>
    {
        public void Configure(EntityTypeBuilder<ShoppingCartProducts> builder)
        {
            #region ShoppingCartProducts Setup

            builder.ToTable("ShoppingCartProducts");

            builder.HasKey(p => new {p.ProductId, p.ShoppingCartId});

            builder.Property(s => s.ShoppingCartId)
                .IsRequired(true);

            builder.Property(s => s.ProductId)
                .IsRequired(true);

            #endregion
        }
    }
}
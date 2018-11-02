using GameStore.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameStore.Data.Context.Configurations
{
    internal class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            #region Product Setup

            builder.ToTable("Products");

            builder.HasIndex(p => p.Name)
                .IsUnique(true);
            builder.Property(p => p.ProductImageName)
                  .IsRequired(false);

            builder.Property(p => p.Name)
                .HasMaxLength(70)
                .IsRequired(true);

            builder.Property(p => p.Description)
                .HasMaxLength(100)
                .IsRequired(true);

            builder.Property(p => p.Price)
                .IsRequired(true);

            #endregion
        }
    }
}
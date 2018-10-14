using GameStore.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameStore.Data.Context.Configurations
{
    internal class OrderProductsConfiguration : IEntityTypeConfiguration<OrderProducts>
    {
        public void Configure(EntityTypeBuilder<OrderProducts> builder)
        {
            #region OrderProducts Setup

            builder.ToTable("OrdersProducts");

            builder.HasKey(o => new {o.OrderId, o.ProductId});

            builder.Property(o => o.OrderId)
                .IsRequired(true);
            builder.Property(o => o.ProductId)
                .IsRequired(true);

            #endregion
        }
    }
}
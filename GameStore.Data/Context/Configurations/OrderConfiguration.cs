using GameStore.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameStore.Data.Context.Configurations
{
    internal class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            #region Order Setup

            builder.ToTable("Orders");

            builder.Property(o => o.AccountId)
                .IsRequired(true);

            #endregion
        }
    }
}
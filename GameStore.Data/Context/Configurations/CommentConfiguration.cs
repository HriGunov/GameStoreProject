using GameStore.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameStore.Data.Context.Configurations
{
    internal class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            #region Comment Setup
            builder.ToTable("Comments");

            builder.Property(c => c.AccountId)
                        .IsRequired(true);

            builder.Property(c => c.ProductId)
                        .IsRequired(true);

            builder.Property(c => c.Text)
                        .HasMaxLength(100)
                        .IsRequired(true);
            #endregion
        }
    }
}
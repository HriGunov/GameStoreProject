using GameStore.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameStore.Data.Context.Configurations
{
    internal class GenreConfiguration : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> builder)
        {
            #region Genre Setup
            builder.ToTable("Genres");

            builder.Property(g => g.Name)
                        .IsRequired(true);
            #endregion
        }
    }
}
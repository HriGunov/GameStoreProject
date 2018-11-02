using GameStore.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameStore.Data.Context.Configurations
{
    internal class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            #region Account Setup

            builder.ToTable("Accounts");

            builder.Property(x => x.AvatarImageName)
                .HasMaxLength(100)
               .IsRequired(false);
            builder.Property(x => x.FirstName)
                .HasMaxLength(20)
                .IsRequired(false);

            builder.Property(x => x.LastName)
                .HasMaxLength(20)
                .IsRequired(false);

            #endregion
        }
    }
}
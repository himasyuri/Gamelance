using Gamelance.Models.Users;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gamelance.Data.Configurations
{
    public class UserPageConfiguration : IEntityTypeConfiguration<UserPage>
    {
        public void Configure(EntityTypeBuilder<UserPage> builder)
        {
            builder.HasKey(p => p.PageId);

            builder.Property(p => p.Status)
                .HasMaxLength(100);

            builder.Property(p => p.About)
                .HasMaxLength(150);
        }
    }
}

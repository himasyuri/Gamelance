using Gamelance.Models.Posts;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gamelance.Data.Configurations
{
    public class OrganizationPostConfiguration : IEntityTypeConfiguration<OrganizationPost>
    {
        public void Configure(EntityTypeBuilder<OrganizationPost> builder)
        {
            builder.HasKey(p => p.PostId);

            builder.Property(p => p.Title)
                .HasMaxLength(50);
        }
    }
}

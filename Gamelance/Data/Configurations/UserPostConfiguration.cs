using Gamelance.Models.Posts;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gamelance.Data.Configurations
{
    public class UserPostConfiguration : IEntityTypeConfiguration<UserPost>
    {
        public void Configure(EntityTypeBuilder<UserPost> builder)
        {
            builder.HasKey(p => p.PostId);

            builder.Property(p => p.Title)
                .HasMaxLength(50);

        }
    }
}

using Gamelance.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gamelance.Data.Configurations
{
    public class OrganizationPageConfiguration : IEntityTypeConfiguration<OrganizationPage>
    {
        public void Configure(EntityTypeBuilder<OrganizationPage> builder)
        {
            builder.HasKey(p => p.PageId);

            builder.Property(p => p.Status)
                .HasMaxLength(100);

            builder.Property(p => p.Tag)
                .HasMaxLength(10);

            builder.Property(p => p.Description)
                   .HasMaxLength(200);
            
            builder.HasOne(p => p.Creator)
                   .WithOne()
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }
}

using Gamelance.Models.Users;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gamelance.Data.Configurations
{
    public class ParticipiantConfiguration : IEntityTypeConfiguration<Partipiciant>
    {
        public void Configure(EntityTypeBuilder<Partipiciant> builder)
        {
            builder.HasKey(p => p.Id);

            builder.HasOne(p => p.Organization)
                   .WithMany()
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(p => p.User)
                   .WithOne()
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(p => p.AddedBy)
                   .WithMany()
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }
}

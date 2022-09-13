using Gamelance.Models.Applications;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gamelance.Data.Configurations
{
    public class ChangeUserNameAppsStoreConfiguration : IEntityTypeConfiguration<ChangeUserNameApps>
    {
        public void Configure(EntityTypeBuilder<ChangeUserNameApps> builder)
        {
            builder.HasKey(p => p.Id);
        }
    }
}

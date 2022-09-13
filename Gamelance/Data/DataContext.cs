
using Gamelance.Models;
using Gamelance.Models.Users;
using Gamelance.Models.Posts;
using Gamelance.Models.Reviews;
using Gamelance.Data.Configurations;
using Gamelance.Models.Photos;
using Gamelance.Models.Applications;

namespace Gamelance.Data
{
    public class DataContext : DbContext 
    {
        public DbSet<Game> Games { get; set; } = null!;

        public DbSet<OfferCategory> OfferCategories { get; set; } = null!;

        public DbSet<Offer> Offers { get; set; } = null!;

        public DbSet<UserPage> UserPages { get; set; } = null!;

        public DbSet<OrganizationPage> OrganizationPages { get; set; } = null!;

        public DbSet<UserPost> UserPosts { get; set; } = null!;

        public DbSet<OrganizationPost> OrganizationPosts { get; set; } = null!;

        public DbSet<Review> Reviews { get; set; } = null!;

        public DbSet<Photo> Photos { get; set; } = null!;

        public DbSet<Partipiciant> Partipiciants { get; set; } = null!;

        public DbSet<ChangeUserNameApps> ChangeUserNameAppsStore { get; set; } = null!;

        public DataContext(DbContextOptions options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<OfferCategory>()
                .HasKey(p => p.CategoryId);

            builder.ApplyConfiguration(new UserPageConfiguration());
            builder.ApplyConfiguration(new UserPostConfiguration());
            builder.ApplyConfiguration(new OrganizationPostConfiguration());
            builder.ApplyConfiguration(new OrganizationPageConfiguration());
            builder.ApplyConfiguration(new ParticipiantConfiguration());
            builder.ApplyConfiguration(new ChangeUserNameAppsStoreConfiguration());
        }
    }
}

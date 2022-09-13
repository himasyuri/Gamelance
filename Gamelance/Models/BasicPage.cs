using Gamelance.Models.Photos;

namespace Gamelance.Models
{
    public abstract class BasicPage
    {
        public Guid PageId { get; set; } = Guid.NewGuid();

        public Photo? Avatar { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public bool IsDeleted { get; set; } = false;

        public DateTime DeletedAt { get; set; }

        public string Status { get; set; } = string.Empty;

        public bool IsBanned { get; set; } = false;
    }
}

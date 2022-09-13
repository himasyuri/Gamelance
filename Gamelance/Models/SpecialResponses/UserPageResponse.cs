using Gamelance.Models.Posts;
using Gamelance.Models.Reviews;
using Gamelance.Models.Users;

namespace Gamelance.Models.SpecialResponses
{
    public class UserPageResponse
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public double Reputation { get; set; }

        public bool IsFindingStatus { get; set; } = false;

        public bool IsOnline { get; set; }

        public string About { get; set; } = string.Empty;

        public Guid OrgPageId { get; set; }

        public string OrganizationName { get; set; } = string.Empty;

        public DateTime LastOnline { get; set; }

        public bool IsBanned { get; set; }

        public bool IsDeleted { get; set; }

        public List<UserPost> UserPosts { get; set; } = new List<UserPost>();

        public List<Review> Reviews { get; set; } = new List<Review>();

        public byte[]? ImageData { get; set; }
    }
}

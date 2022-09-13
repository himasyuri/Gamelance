using Gamelance.Models.Posts;
using Gamelance.Models.Reviews;

namespace Gamelance.Models.Users
{
    public class UserPage : BasicPage
    {
        //PageId equals UserId

        public string Name { get; set; } = string.Empty;

        public double Reputation { get; set; }

        //Is user finding team or not
        public bool IsFindingStatus { get; set; } = false;

        public bool IsOnline { get; set; }

        public string About { get; set; } = string.Empty;

        public Guid OrgPageId { get; set; }

        public OrganizationPage? OrganizationPage { get; set; }

        public DateTime LastOnline { get; set; } 

        public List<UserPost> UserPosts { get; set; } = new List<UserPost>();

        public List<Review> Reviews { get; set; } = new List<Review>();
    }
}

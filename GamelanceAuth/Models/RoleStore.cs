namespace GamelanceAuth.Models
{
    public class RoleStore
    {
        public int RoleId { get; set; }
        public List<Role> Roles { get; set; } = new List<Role>();
    }
}

namespace GamelanceAuth.Models
{
    public class UserRole
    {
        public int Id { get; set; }

        public User? User { get; set; } 

        public Guid UserId { get; set; } = default!;

        public Role? Role { get; set; }
        public int RoleId { get; set; } = default!;
    }
}

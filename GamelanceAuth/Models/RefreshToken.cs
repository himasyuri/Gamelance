namespace GamelanceAuth.Models
{
    public class RefreshToken
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Token { get; set; } = string.Empty;

        public User? User { get; set; }

        public DateTime Exspires { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public string? UserAgentHash { get; set; }

        public bool IsExpired => DateTime.UtcNow >= Exspires;

        public bool IsRevoked { get; set; } = false;

        public bool IsActive => !IsRevoked && !IsExpired;
    }
}

namespace Gamelance.Models.Applications
{
    public class ChangeUserNameApps
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string NewName { get; set; } = string.Empty;

        public Guid UserId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public bool IsApprove { get; set; } = false;

        public bool IsSeen { get; set; } = false;
    }
}

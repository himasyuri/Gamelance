using System.ComponentModel.DataAnnotations;

namespace Gamelance.Models.Dto
{
    public class GameDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;
    }
}

using System.ComponentModel.DataAnnotations;

namespace Gamelance.Models.Dto
{
    public class UserPageRegistration
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        //public IFormFile? Photo { get; set; }

        [MaxLength(150)]
        public string About { get; set; } = string.Empty;
    }
}

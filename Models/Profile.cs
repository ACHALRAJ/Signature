using System.ComponentModel.DataAnnotations;

namespace SignatureMain.Models
{
    public class Profile
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(10), MaxLength(10)]
        [Phone]
        public string Phone { get; set; }

        public string Signature { get; set; }
    }
}

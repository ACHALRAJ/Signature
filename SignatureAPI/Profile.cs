using System.ComponentModel.DataAnnotations;

namespace SignatureAPI
{
    public class Profile
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Phone]
        public string Phone { get; set; }

        public string Signature { get; set; }
    }
}

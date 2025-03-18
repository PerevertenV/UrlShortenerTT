using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace UrlShortener.Models
{
    public class UserModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        [DisplayName("Login")]
        public string Login { get; set; } = default!;

        [Required]
        [DisplayName("Password")]
        public string Password { get; set; } = default!;

        [Required]
        public string Role { get; set; } = default!;
    }
}

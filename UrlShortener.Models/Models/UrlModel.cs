using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace UrlShortener.Models
{
    public class UrlModel
    {
        [Required]
        public int Id { get; set; }

        public UserModel User { get; set; } = default!;

        [Required]
        [DisplayName("Long url")]
        public string LongUrl { get; set; } = default!;

        [Required]
        [DisplayName("Generated short url")]
        public string ShortUrl { get; set; } = default!;

        [Required]
        [DisplayName("Generation date")]
        public DateOnly CreatedDate { get; set; }
    }
}

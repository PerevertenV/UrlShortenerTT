using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace UrlShortener.DataAccess.Entities
{
    public class UrlEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int IdOfUser { get; set; }
        [ForeignKey(nameof(IdOfUser))]
        public UserEntity User { get; set; } = default!;

        [Required]
        public string LongUrl { get; set; } = default!;

        [Required]
        public string ShortUrl { get; set; } = default!;

        [Required]
        public DateOnly CreatedDate { get; set; }
    }
}

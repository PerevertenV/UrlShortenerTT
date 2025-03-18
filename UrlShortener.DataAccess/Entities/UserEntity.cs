using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrlShortener.DataAccess.Entities
{
    public class UserEntity
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string Login { get; set; } = default!;

        [Required]
        public string Password { get; set; } = default!;

        public string Role { get; set; } = default!;
    }
}

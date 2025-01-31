using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UsuariosAPI.Models
{
    [Table("users")]
    public class User
    {
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column ("name")]
        public required string Name { get; set; }

        [Required]
        [EmailAddress]
        [Column("email")]
        public required string Email { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }
    }
}
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SaleManagerWebAPI.Models.Entities
{
    public class Account
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("username")]
        public string Username { get; set; }

        [Required]
        [MaxLength(100)]
        [EmailAddress]
        [Column("email")]
        public string Email { get; set; }

        [Required]
        [MaxLength(255)]
        [Column("password_hash")]
        public string PasswordHash { get; set; }

        [MaxLength(100)]
        [Column("full_name")]
        public string? FullName { get; set; }

        [Column("last_login")]
        public DateTime? LastLogin { get; set; }

        public virtual ICollection<AuthToken> AuthTokens { get; set; } = new List<AuthToken>();
    }

}

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SaleManagerWebAPI.Models.Entities
{
    public class AuthToken
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [Column("account_id")]
        public Guid AccountId { get; set; }

        [Required]
        [MaxLength(255)]
        [Column("token")]
        public string Token { get; set; }

        [Required]
        [Column("token_type")]
        [MaxLength(20)]
        public string TokenType { get; set; }
        [Required]
        [Column("expires_at")]
        public DateTime ExpiresAt { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Column("last_used")]
        public DateTime? LastUsed { get; set; }

        [MaxLength(255)]
        [Column("device_info")]
        public string? DeviceInfo { get; set; }

        [MaxLength(45)]
        [Column("ip_address")]
        public string? IpAddress { get; set; }

        [Column("is_active")]
        public bool IsActive { get; set; } = true;

        // Navigation property
        [ForeignKey("AccountId")]
        public virtual Account Account { get; set; }
    }
}

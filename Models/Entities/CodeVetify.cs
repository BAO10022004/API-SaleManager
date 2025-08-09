using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SaleManagerWebAPI.Models.Entities
{
    public class CodeVetify
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [Column("account_id")]
        public Guid AccountId { get; set; }

        [Required]
        [MaxLength(255)]
        [Column("code")]
        public string Code { get; set; }

        [Required]
        [Column("expires_at")]
        public DateTime ExpiresAt { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [Column("verified_at ")]
        public DateTime? VerifiedAt { get; set; }

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

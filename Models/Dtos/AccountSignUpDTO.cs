using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SaleManagerWebAPI.Models.Dtos
{
    public class AccountSignUpDTO
    {
        [MaxLength(20)]
        public string Username { get; set; }

        [MaxLength(100)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MaxLength(20)]
        public string Password { get; set; }

    }
}



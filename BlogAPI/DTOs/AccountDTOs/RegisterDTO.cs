using System.ComponentModel.DataAnnotations;

namespace BlogAPI.DTOs.AccountDTOs
{
    public class RegisterDTO
    {
        [MaxLength(100)]
        public string Firstname { get; set; }

        [MaxLength(100)]
        public string Lastname { get; set; }

        [MaxLength(50)]
        public string Username { get; set; }

        [MaxLength(200)]
        [EmailAddress]
        public string Email { get; set; }

        [MaxLength(200)]
        public string Password { get; set; }
    }
}

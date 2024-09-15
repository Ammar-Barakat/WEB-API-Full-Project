using System.ComponentModel.DataAnnotations;

namespace BlogAPI.DTOs.AccountDTOs
{
    public class LoginDTO
    {
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
    }
}

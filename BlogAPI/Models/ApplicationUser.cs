using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BlogAPI.Models
{
    public class ApplicationUser : IdentityUser
    {
        [MaxLength(100)]
        public string Firstname { get; set; }
        [MaxLength(100)]
        public string Lastname { get; set; }

        public List<Post> Posts { get; set; }
        public List<Comment> Comments { get; set; }
    }
}

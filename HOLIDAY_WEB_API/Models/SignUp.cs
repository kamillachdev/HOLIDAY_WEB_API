using System.ComponentModel.DataAnnotations;

namespace HOLIDAY_WEB_API.Models
{
    public class SignUpRequest
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Role { get; set; }
        [Required]
        public string Email { get; set; }
    }
}

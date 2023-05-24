using System.ComponentModel.DataAnnotations;

namespace HOLIDAY_WEB_API.Models
{
    public class SignInRequest
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Role { get; set; }
    }
}

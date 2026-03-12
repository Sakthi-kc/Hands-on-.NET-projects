using System.ComponentModel.DataAnnotations;

namespace Tango.Employee.DTOs
{
    public class LoginDTO
    {
        [Required]
        public required string Username { get; set; }
        
        [Required]
        public required string Password { get; set; }
    }
}

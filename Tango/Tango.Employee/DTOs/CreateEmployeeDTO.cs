using System.ComponentModel.DataAnnotations;

namespace Tango.Employee.DTOs
{
    public class CreateEmployeeDTO
    {
        [Required]
        public string? EmployeeName { get; set; }

        public string? Department { get; set; }

        [StringLength(3, MinimumLength = 3, ErrorMessage = "Location must be exactly 3 chars")]
        public string? Location { get; set; }
    }
}

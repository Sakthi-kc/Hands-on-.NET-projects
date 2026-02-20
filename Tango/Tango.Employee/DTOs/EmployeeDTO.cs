using System.ComponentModel.DataAnnotations;

namespace Tango.Employee.DTOs
{
    public class EmployeeDTO
    {
        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string Department { get; set; }
        
        [StringLength(3, MinimumLength = 3, ErrorMessage = "Location must be exactly 3 chars")]
        public string Location { get; set; }

    }
}

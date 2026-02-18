using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tango.Employee.Entities
{
    //replicates DB table
    public class EmployeeEntityModel
    {
        public int EmployeeID { get; set; }
        public string? EmployeeName { get; set; }
        public string? Department { get; set; }
        public string? CityCode { get; set; }
    }
}

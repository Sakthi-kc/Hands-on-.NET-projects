using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Tango.Employee.Data;
using Tango.Employee.DTOs;

namespace Tango.Employee.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public class EmployeeController : ControllerBase
    {
        private readonly ILogger<EmployeeController> _logger;
        private readonly TangoDBContext _context;

        public EmployeeController(ILogger<EmployeeController> logger, TangoDBContext context)
        {
            _logger = logger;
            _context = context;
            _logger.LogTrace("Note: Constructor created");
        }

        [HttpGet("/")]
        public bool CheckEndpoint()
        {
            _logger.LogInformation("Endpoint is working");
            return true;
        }

        [HttpGet]
        [Route("/api/Employees")]
        public ActionResult<IEnumerable<EmployeeDTO>> GetEmployees()
        {
            return Ok(_context.Employees);
        }

        [HttpGet]
        [Route("{id:int}")]
        public ActionResult<EmployeeDTO> GetEmployeeById(int id)
        {
            var employee = _context.Employees
                .Where(emp => emp.EmployeeID == id)
                .FirstOrDefault();

            if (employee == null)
                return BadRequest($"No employee exists with this id {id}");

            return Ok(employee);
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteEmployee(int id)
        {
            var employee = _context.Employees
                .FirstOrDefault(emp => emp.EmployeeID == id);

            if(employee == null)
                return BadRequest($"No employee exists with this id {id}");

            _context.Employees.Remove(employee);
            _context.SaveChanges();

            return Ok();
        }

        [HttpPost]
        [Route("Create")]
        public ActionResult<EmployeeDTO> CreateEmployee([FromBody] CreateEmployeeDTO body)
        {
            var newEmployee = new Entities.EmployeeEntityModel
            {
                EmployeeName = body.EmployeeName,
                Department = body.Department,
                CityCode = body.Location
            };

            _context.Employees.Add(newEmployee);
            _context.SaveChanges();

            int id = newEmployee.EmployeeID;

            var responseBody = new EmployeeDTO
            {
                EmployeeID = newEmployee.EmployeeID,
                EmployeeName = newEmployee.EmployeeName,
                Department = newEmployee.Department,
                Location = newEmployee.CityCode
            };

            //return Created($"Employee/{newId}", newEmployee);
            return CreatedAtAction(nameof(GetEmployeeById), new { id = id }, responseBody);
        }

        //update all the fields
        //if any field not sent, it will be updated with default value as 0 or null
        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(204)]
        public IActionResult UpdateEmployee(int id, [FromBody] UpdateEmployeeDTO body)
        {
            var employee = _context.Employees
                .Where(emp => emp.EmployeeID == id).FirstOrDefault();

            if(employee == null)
                return NotFound();

            employee.EmployeeName = body.EmployeeName;
            employee.CityCode = body.Location;
            _context.SaveChanges();
            return NoContent();
        }

        //updates partial records
        [HttpPatch("{id}")]
        public ActionResult<EmployeeDTO> UpdateEmployeePartial(int id, 
            [FromBody] JsonPatchDocument<EmployeeDTO> body)
        {
            if (body == null || id <= 0)
                return BadRequest();

            var employee = _context.Employees
                .Where(emp => emp.EmployeeID == id).FirstOrDefault();

            if (employee == null)
                return NotFound();

            //create a copy to validate
            var employeeRecord = new EmployeeDTO
            {
                EmployeeID = id,
                EmployeeName = employee.EmployeeName,
                Location = employee.CityCode,
                Department = employee.Department
            };

            body.ApplyTo(employeeRecord, ModelState);
            TryValidateModel(employeeRecord);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            employee.CityCode = employeeRecord.Location;
            _context.SaveChanges();

            return Ok(employee);
        }
    }
}

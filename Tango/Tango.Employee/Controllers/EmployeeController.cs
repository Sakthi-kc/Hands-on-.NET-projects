using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tango.Employee.DTOs;
using Tango.Employee.Services;

namespace Tango.Employee.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public class EmployeeController : ControllerBase
    {
        private readonly ILogger<EmployeeController> _logger;
        private readonly IEmployeeService _service;

        public EmployeeController(ILogger<EmployeeController> logger, IEmployeeService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet("/")]
        public bool CheckEndpoint()
        {
            _logger.LogInformation("Endpoint is working");
            return true;
        }


        [HttpGet]
        [Route("/api/Employees")]
        public async Task<ActionResult<IEnumerable<EmployeeDTO>>> GetEmployees()
        {
            //when we want to read data but EFCore wont track this object, saves memory
            var employees = await _service.GetAllRecordsAsync();

            //if we are projecting to a DTO with select, tracking would not apply to the DTO, so not required

            //.Select(emp => new EmployeeDTO
            //{
            //    EmployeeID = emp.EmployeeID,
            //    EmployeeName = emp.EmployeeName,
            //    Department = emp.Department,
            //    Location = emp.CityCode
            //})


            return Ok(employees);
        }


        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<EmployeeDTO>> GetEmployeeById(int id)
        {
            var employee = await _service.GetByIdNoTrackingAsync(id);
            return Ok(employee);
        }


        [HttpGet]
        [Route("Department/{dept}")]
        public async Task<ActionResult<List<EmployeeDTO>>> GetEmployeeByDept(string dept)
        {
            var employee = await _service.GetByDeptAsync(dept);
            return Ok(employee);
        }


        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {

            await _service.DeleteRecordAsync(id);
            return Ok();

        }


        [HttpPost]
        [Route("Create")]
        public async Task<ActionResult<EmployeeDTO>> CreateEmployee([FromBody] CreateEmployeeDTO body)
        {

            var newEmployee = await _service.CreateRecordAsync(body);

            //the return Created($"Employee/{newId}", newEmployee);
            //route value expects object and not primitive types

            return CreatedAtAction(nameof(GetEmployeeById), new { id = newEmployee.EmployeeID }, newEmployee);

        }


        //update all the fields
        //if any field not sent, it will be updated with default value as 0 or null
        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> UpdateEmployee(int id, [FromBody] UpdateEmployeeDTO body)
        {
            await _service.UpdateRecordAsync(id, body);
            return NoContent();
        }



        //updates partial records
        
        /*

        [HttpPatch("{id}")]
        public async Task<ActionResult<EmployeeDTO>> UpdateEmployeePartial(int id, 
            [FromBody] JsonPatchDocument<PartialUpdateDTO> body)
        {
            if (body == null || id <= 0)
                return BadRequest();

            var employee = await _employeeRepo.GetByIdAsync(id);

            if (employee == null)
                return NotFound($"No employee exists with this id {id}");

            //create a copy to apply changes and validate
            var employeeRecord = _mapper.Map<PartialUpdateDTO>(employee);

            //ApplyTo updates only the properties in path
            //if the op and path passed is incorrect error will be added to modelState
            body.ApplyTo(employeeRecord, ModelState);

            //This validates against the annotations against PartialUpdateDTO as the object is created of this type
            TryValidateModel(employeeRecord);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //this updates the existing employee object with received property values and EF marking the entity as modified
            _mapper.Map(employeeRecord, employee);

            await _employeeRepo.SaveChangesAsync();

            return Ok(employee);
        }
        */

    }
}

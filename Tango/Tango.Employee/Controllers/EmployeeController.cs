using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Data;
using Tango.Employee.Data;
using Tango.Employee.Data.Repositories;
using Tango.Employee.DTOs;
using Tango.Employee.Entities;

namespace Tango.Employee.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public class EmployeeController : ControllerBase
    {
        private readonly ILogger<EmployeeController> _logger;
        private readonly IMapper _mapper;
        private readonly IEmployeeRepo _employeeRepo;

        public EmployeeController(ILogger<EmployeeController> logger, IMapper mapper, IEmployeeRepo employeeRepo)
        {
            _logger = logger;
            _mapper = mapper;
            _employeeRepo = employeeRepo;

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
        public async Task<ActionResult<IEnumerable<EmployeeDTO>>> GetEmployees()
        {
            //when we want to read data but EFCore wont track this object, saves memory
            var employees = await _employeeRepo.GetAllRecordsAsync();

                //if we are projecting to a DTO with select, tracking would not apply to the DTO, so not required

                //.Select(emp => new EmployeeDTO
                //{
                //    EmployeeID = emp.EmployeeID,
                //    EmployeeName = emp.EmployeeName,
                //    Department = emp.Department,
                //    Location = emp.CityCode
                //})

            var employeesDTO = _mapper.Map<List<EmployeeDTO>>(employees);
            return Ok(employeesDTO);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<EmployeeDTO>> GetEmployeeById(int id)
        {
            var employee = await _employeeRepo.GetByIdNoTrackingAsync(emp => emp.EmployeeID == id);

            if (employee == null)
                return NotFound($"No employee exists with this id {id}");

            var employeeDTO = _mapper.Map<EmployeeDTO>(employee);

            return Ok(employee);
        }

        [HttpGet]
        [Route("{dept}")]
        public async Task<ActionResult<List<EmployeeDTO>>> GetEmployeeByDept(string dept)
        {
            var employee = await _employeeRepo.GetByDeptAsync(emp => emp.Department == dept);

            if (employee == null)
                return NotFound($"No employee exists in this {dept}");

            var employeeDTO = _mapper.Map<List<EmployeeDTO>>(employee);

            return Ok(employee);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var employee = await _employeeRepo.GetByIdAsync(id);

            if(employee == null)
                return NotFound($"No employee exists with this id {id}");

            await _employeeRepo.DeleteRecordAsync(employee);
            await _employeeRepo.SaveChangesAsync();
            return Ok();
        }

        [HttpPost]
        [Route("Create")]
        public async Task<ActionResult<EmployeeDTO>> CreateEmployee([FromBody] CreateEmployeeDTO body)
        {
            var newEmployee = _mapper.Map<EmployeeEntityModel>(body);

            //Add should come first for tracking and will track id with default 0 for temporary
            //We can add anotherEmployee which also will have id 0 but thir state is added so no error thrown
            await _employeeRepo.CreateRecordAsync(newEmployee);

            //Updates DB with the tracked changes and since id is identity column, DB will generate it
            //EF automatically updates newEmployee with the new id generated
            await _employeeRepo.SaveChangesAsync();

            int id = newEmployee.EmployeeID;

            var responseBody = _mapper.Map<EmployeeDTO>(newEmployee);

            //the return Created($"Employee/{newId}", newEmployee);
            //route value expects object and not primitive types
            return CreatedAtAction(nameof(GetEmployeeById), new {id = id}, responseBody);
        }

        //update all the fields
        //if any field not sent, it will be updated with default value as 0 or null
        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> UpdateEmployee(int id, [FromBody] UpdateEmployeeDTO body)
        {
            //EFCore tracks this entity instance but state is unchanged
            var employee = await _employeeRepo.GetByIdAsync(id);

            if(employee == null)
                return NotFound($"No employee exists with this id {id}");

            //map(source, destination) here the entity state becomes updated
            _mapper.Map(body, employee);

            //add will make the state as added and DB will try to insert causing ID error
            await _employeeRepo.SaveChangesAsync();

            return NoContent();
        }

        //updates partial records
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
    }
}

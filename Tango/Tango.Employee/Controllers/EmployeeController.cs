using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Data;
using Tango.Employee.Data;
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
        private readonly TangoDBContext _context;
        private readonly IMapper _mapper;

        public EmployeeController(ILogger<EmployeeController> logger, TangoDBContext context, IMapper mapper)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;

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
            var employees = await _context.Employees
                .AsNoTracking()
                .ToListAsync();

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
            var employee = await _context.Employees
                .AsNoTracking()
                .FirstOrDefaultAsync(emp => emp.EmployeeID == id);

            if (employee == null)
                return NotFound($"No employee exists with this id {id}");

            var employeeDTO = _mapper.Map<EmployeeDTO>(employee);

            return Ok(employee);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            try
            { 
                //Relational DB delete action can happen with PKey no additional details required
                //this creates an object and sets EmployeeId with given Id
                var employee = new Entities.EmployeeEntityModel
                {
                    EmployeeID = id
                };

                //If the entity is not already tracked, EF attaches it
                //and marks its state as Deleted in the ChangeTracker
                _context.Employees.Remove(employee);

                //this generates and executes the DELETE SQL statement against the database
                //SaveChangesAsync() processes all tracked changes
            
                await _context.SaveChangesAsync();

                return Ok();
            }
            catch(Exception)
            {
                return NotFound($"No employee exists with this id {id}");
            }
        }

        [HttpPost]
        [Route("Create")]
        public async Task<ActionResult<EmployeeDTO>> CreateEmployee([FromBody] CreateEmployeeDTO body)
        {
            var newEmployee = _mapper.Map<EmployeeEntityModel>(body);

            //Add should come first for tracking and will track id with default 0 for temporary
            //We can add anotherEmployee which also will have id 0 but thir state is added so no error thrown
            _context.Employees.Add(newEmployee);

            //Updates DB with the tracked changes and since id is identity column, DB will generate it
            await _context.SaveChangesAsync();

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
            var employee = await _context.Employees
                .FirstOrDefaultAsync(emp => emp.EmployeeID == id);

            if(employee == null)
                return NotFound();

            //map(source, destination) here the entity state becomes updated
            _mapper.Map(body, employee);

            //add will make the state as added and DB will try to insert causing ID error
            await _context.SaveChangesAsync();

            return NoContent();
        }

        //updates partial records
        [HttpPatch("{id}")]
        public async Task<ActionResult<EmployeeDTO>> UpdateEmployeePartial(int id, 
            [FromBody] JsonPatchDocument<PartialUpdateDTO> body)
        {
            if (body == null || id <= 0)
                return BadRequest();

            var employee = await _context.Employees
                .Where(emp => emp.EmployeeID == id).FirstOrDefaultAsync();

            if (employee == null)
                return NotFound($"No employee exists with this id {id}");

            //create a copy to apply changes and validate
            var employeeRecord = _mapper.Map<PartialUpdateDTO>(employee);

            //ApplyTo updates only the properties in path
            //if the op and path passed is incorrect error will be added to modelState
            body.ApplyTo(employeeRecord, ModelState);
            TryValidateModel(employeeRecord);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _mapper.Map(employeeRecord, employee);

            await _context.SaveChangesAsync();

            return Ok(employee);
        }
    }
}

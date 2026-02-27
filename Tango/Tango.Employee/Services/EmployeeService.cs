using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Linq.Expressions;
using System.Numerics;
using System.Threading.Tasks;
using Tango.Employee.Data;
using Tango.Employee.Data.Repositories;
using Tango.Employee.DTOs;
using Tango.Employee.Entities;

namespace Tango.Employee.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IMapper _mapper;
        private readonly IEmployeeRepo _repo;

        public EmployeeService(IEmployeeRepo repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
        }

        public async Task<EmployeeDTO> CreateRecordAsync(CreateEmployeeDTO body)
        {
            var newEmployee = _mapper.Map<EmployeeEntityModel>(body);


            //Add should come first for tracking and will track id with default 0 for temporary
            //We can add anotherEmployee which also will have id 0 but thir state is Added so no error thrown
            _repo.CreateRecord(newEmployee);


            //Updates DB with the tracked changes and since id is identity column, DB will generate it
            //EF automatically updates newEmployee with the new id generated
            await _repo.SaveChangesAsync();

            return _mapper.Map<EmployeeDTO>(newEmployee);
        }

        public async Task DeleteRecordAsync(int id)
        {
            var employee = await _repo.GetByIdAsync(id);

            if (employee == null)
                throw new Exception($"No employee exists with this id {id}");

            _repo.DeleteRecord(employee);

            await _repo.SaveChangesAsync();
        }

        public async Task<List<EmployeeDTO>> GetAllRecordsAsync()
        {
            var employees = await _repo.GetAllRecordsAsync();

            return _mapper.Map<List<EmployeeDTO>>(employees);
        }

        public async Task<EmployeeDTO> GetByIdAsync(int id)
        {
            var employee = await _repo.GetByIdAsync(id);

            if (employee == null)
                throw new Exception($"No employee exists with this id {id}");

            return _mapper.Map<EmployeeDTO>(employee);

        }

        public  async Task<EmployeeDTO> GetByIdNoTrackingAsync(int id)
        {
            var employee = await _repo.GetByIdNoTrackingAsync(emp => emp.EmployeeID == id);

            if (employee == null)
                throw new Exception($"No employee exists with this id {id}");

            return _mapper.Map<EmployeeDTO>(employee);
        }

        public async Task<List<EmployeeDTO>> GetByDeptAsync(string dept)
        {
            var employees = await _repo.GetByDeptAsync(emp => emp.Department == dept);

            if (employees == null)
                throw new Exception($"No employees exist in this department {dept}");

            return _mapper.Map<List<EmployeeDTO>>(employees);
        }

        public async Task UpdateRecordAsync(int id, UpdateEmployeeDTO body)
        {
            //EFCore tracks this entity instance but state is unchanged
            var employee = await _repo.GetByIdAsync(id);

            if (employee == null)
                throw new Exception($"No employee exists with this id {id}");

            //map(source, destination) here the entity state becomes updated
            _mapper.Map(body, employee);

            _repo.SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _repo.SaveChangesAsync();
        }

    }
}

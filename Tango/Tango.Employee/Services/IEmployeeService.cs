using System.Linq.Expressions;
using Tango.Employee.Data.Repositories;
using Tango.Employee.DTOs;
using Tango.Employee.Entities;

namespace Tango.Employee.Services
{
    public interface IEmployeeService
    {
        Task <List<EmployeeDTO>> GetAllRecordsAsync();

        Task<EmployeeDTO> GetByIdNoTrackingAsync(int id);

        Task<EmployeeDTO> GetByIdAsync(int id);

        Task DeleteRecordAsync(int id);

        Task<EmployeeDTO> CreateRecordAsync(CreateEmployeeDTO body);

        Task UpdateRecordAsync(int id, UpdateEmployeeDTO body);


        Task SaveChangesAsync();

        Task<List<EmployeeDTO>> GetByDeptAsync(string dept);
    }
}

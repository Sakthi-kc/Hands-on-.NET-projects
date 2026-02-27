using Microsoft.AspNetCore.JsonPatch;
using System.Linq.Expressions;
using Tango.Employee.DTOs;
using Tango.Employee.Entities;

namespace Tango.Employee.Data.Repositories
{
    public interface ITangoRepo<T>
    {
        //Task<List<EmployeeEntityModel>> GetAllRecordsAsync();
        Task<List<T>> GetAllRecordsAsync();

        Task<T> GetByIdNoTrackingAsync(Expression<Func<T, bool>> filters);

        Task<T> GetByIdAsync(int id);

        void DeleteRecord(T body);

        void CreateRecord(T body);

        Task SaveChangesAsync();
    }
}

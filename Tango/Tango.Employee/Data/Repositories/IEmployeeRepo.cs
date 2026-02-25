using System.Linq.Expressions;
using Tango.Employee.DTOs;
using Tango.Employee.Entities;

namespace Tango.Employee.Data.Repositories
{
    public interface IEmployeeRepo : ITangoRepo<EmployeeEntityModel>
    {
        Task<List<EmployeeEntityModel>> GetByDeptAsync(Expression<Func<EmployeeEntityModel, bool>> filters);
    }
}

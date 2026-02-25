using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Tango.Employee.DTOs;
using Tango.Employee.Entities;

namespace Tango.Employee.Data.Repositories
{
    public class EmployeeRepo : TangoRepo<EmployeeEntityModel>, IEmployeeRepo
    {
        public EmployeeRepo(TangoDBContext context) : base(context)
        {
        }
        public async Task<List<EmployeeEntityModel>> GetByDeptAsync(Expression<Func<EmployeeEntityModel,bool>> filters)
        {
            var employee = await _dbSet
                .Where(filters).ToListAsync();

            return employee;
        }
    }
}

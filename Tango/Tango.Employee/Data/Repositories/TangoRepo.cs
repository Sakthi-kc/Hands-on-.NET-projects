using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Tango.Employee.DTOs;
using Tango.Employee.Entities;

namespace Tango.Employee.Data.Repositories
{
    public class TangoRepo<T> : ITangoRepo<T> where T : class
    {
        protected readonly TangoDBContext _context;
        protected DbSet<T> _dbSet;

        public TangoRepo(TangoDBContext context)
        {
            _context = context;
            //Give me the DbSet (table) for whatever T is
            _dbSet = _context.Set<T>();
        }
        public async Task<List<T>> GetAllRecordsAsync()
        {
            var employees = await _dbSet.ToListAsync();
            return employees;
        }

        public async Task<T> GetByIdNoTrackingAsync(Expression<Func<T, bool>> filters)
        {
            var employees = await _dbSet
                .AsNoTracking()
                .FirstOrDefaultAsync(filters);

            return employees;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var employee = await _dbSet.FindAsync(id);

            return employee;
        }

        public async Task DeleteRecordAsync(T body)
        {
            _dbSet.Remove(body);
        }

        public async Task CreateRecordAsync(T body)
        {
            _dbSet.Add(body);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}

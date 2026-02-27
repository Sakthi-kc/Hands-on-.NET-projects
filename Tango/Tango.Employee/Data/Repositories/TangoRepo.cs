using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Tango.Employee.DTOs;
using Tango.Employee.Entities;

namespace Tango.Employee.Data.Repositories
{
    //EF Core itself requires TEntity (DbSet<TEntity>) to be a class, hence declare where else it will throw compile time error

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
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetByIdNoTrackingAsync(Expression<Func<T, bool>> filters)
        {
            return await _dbSet
                .AsNoTracking()
                .FirstOrDefaultAsync(filters);
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public void DeleteRecord(T body) => _dbSet.Remove(body);

        public void CreateRecord(T body) => _dbSet.Add(body);

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}

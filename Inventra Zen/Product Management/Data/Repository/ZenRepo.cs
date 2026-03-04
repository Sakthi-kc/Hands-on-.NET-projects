using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Product_Management.Data.Repository
{
    public class ZenRepo<T> : IZenRepo<T> where T : class
    {
        protected readonly ProductDBContext _context;
        protected readonly DbSet<T> _dbSet;

        public ZenRepo(ProductDBContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<List<T>> GetAllDataAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T?> GetDataAsync(Expression<Func<T, bool>> filter)
        {
            return await _dbSet
                .AsNoTracking()
                .FirstOrDefaultAsync(filter);
        }

        public async Task<T?> GetDataByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task AddDataAsync(T data)
        {
            await _dbSet.AddAsync(data);
        }

        public void UpdateData(T data)
        {
            _dbSet.Update(data);
        }

        public void DeleteData(T data)
        {
            _dbSet.Remove(data);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

    }
}

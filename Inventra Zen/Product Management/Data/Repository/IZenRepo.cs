using System.Collections.Generic;
using System.Linq.Expressions;

namespace Product_Management.Data.Repository
{
    public interface IZenRepo<T>
    {
        Task<List<T>> GetAllDataAsync();

        Task<T?> GetDataAsync(Expression<Func<T, bool>> filter);

        Task<T?> GetDataByIdAsync(int id);

        Task AddDataAsync(T data);

        void UpdateData(T data);

        void DeleteData(T data);

        Task SaveChangesAsync();

    }
}

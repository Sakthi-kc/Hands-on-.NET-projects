using Product_Management.Entity_Models;
using System.Linq.Expressions;

namespace Product_Management.Data.Repository
{
    public interface IProductRepo : IZenRepo<ProductEntityModel>
    {
        Task<List<ProductEntityModel>> GetActiveProductsAsync();

        Task<List<ProductEntityModel>> GetProductsByCategoryAsync(string categoryName);

        Task<ProductEntityModel?> GetProductByNameAsync(Expression<Func<ProductEntityModel, bool>> filter);
    }
}

using Product_Management.Entity_Models;
using System.Linq.Expressions;

namespace Product_Management.Data.Repository
{
    public interface IProductRepo : IZenRepo<ProductEntityModel>
    {
        Task<List<ProductEntityModel>> GetActiveProductsAsync();

        Task<List<ProductEntityModel>> GetProductsByCategoryAsync(Expression<Func<ProductEntityModel, bool>> filter);

        Task<ProductEntityModel?> GetProductByNameAsync(string productName);
    }
}

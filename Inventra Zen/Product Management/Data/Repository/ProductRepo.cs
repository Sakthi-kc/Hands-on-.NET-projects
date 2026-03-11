using Microsoft.EntityFrameworkCore;
using Product_Management.Entity_Models;
using System.Linq.Expressions;

namespace Product_Management.Data.Repository
{
    public class ProductRepo : ZenRepo<ProductEntityModel>, IProductRepo
    {
        public ProductRepo(ProductDBContext context) : base(context)
        {
            
        }

        public async Task<List<ProductEntityModel>> GetActiveProductsAsync()
        {
            return await _dbSet
                .Where(x => x.IsActive)
                .ToListAsync();
        }

        public async Task<ProductEntityModel?> GetProductByNameAsync(string productName)
        {
            return await _dbSet
                .FirstOrDefaultAsync(n => n.ProductName.Replace(" ", "").ToLower() == productName.Trim().ToLower());
        }

        public async Task<List<ProductEntityModel>> GetProductsByCategoryAsync(Expression<Func<ProductEntityModel, bool>> filter)
        {
            return await _dbSet
                .Where(filter)
                .ToListAsync();
        }


    }
}

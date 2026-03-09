using Product_Management.DTOs;
using Product_Management.Entity_Models;
using System.Linq.Expressions;

namespace Product_Management.Services
{
    public interface IProductService
    {
        Task<List<ProductDTO>> GetAllDataAsync();

        Task<ProductDTO?> GetDataAsync(Expression<Func<ProductDTO, bool>> filter);

        Task<ProductDTO?> GetDataByIdAsync(int id);

        Task AddDataAsync(CreateProductDTO data);

        void UpdateData(UpdateProductDTO data);

        Task<List<ProductDTO>?> GetActiveProducts();

        Task<List<ProductDTO>?> GetProductsByCategory(string categoryName);
    }
}

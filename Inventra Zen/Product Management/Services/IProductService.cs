using Product_Management.DTOs;
using Product_Management.Entity_Models;
using System.Linq.Expressions;

namespace Product_Management.Services
{
    public interface IProductService
    {
        Task<ProductDTO?> GetProductByNameAsync(string productName);

        Task<ProductDTO> AddDataAsync(CreateProductDTO data);

        Task<ProductDTO> UpdateDataAsync(UpdateProductDTO data);

        Task<List<ProductDTO>> GetActiveProductsAsync();

        Task<List<ProductDTO>> GetProductsByCategory(string categoryName);
    }
}

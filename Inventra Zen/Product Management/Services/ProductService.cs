using AutoMapper;
using Product_Management.Data.Repository;
using Product_Management.DTOs;
using Product_Management.Entity_Models;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Product_Management.Services
{
    public class ProductService : IProductService
    {
        protected readonly IMapper _mapper;
        protected readonly IProductRepo _repo;

        public ProductService(IMapper mapper, IProductRepo repo)
        {
            _mapper = mapper;
            _repo = repo;
        }

        public async Task<ProductDTO> AddDataAsync(CreateProductDTO data)
        {
            var newProduct = _mapper.Map<ProductEntityModel>(data);
            await _repo.AddDataAsync(newProduct);

            await _repo.SaveChangesAsync();

            return _mapper.Map<ProductDTO>(newProduct);
        }

        public async Task<List<ProductDTO>> GetActiveProductsAsync()
        {
            var activeProducts = await _repo.GetActiveProductsAsync();

            return _mapper.Map<List<ProductDTO>>(activeProducts);
        }


        public async Task<ProductDTO?> GetDataByNameAsync(string productName)
        {
            var product = await _repo.GetProductByNameAsync(n => n.ProductName == productName);

            return _mapper.Map<ProductDTO>(product);
        }

        public async Task<List<ProductDTO>> GetProductsByCategory(string categoryName)
        {
            var products = await _repo.GetProductsByCategoryAsync(categoryName);

            return _mapper.Map<List<ProductDTO>>(products);
        }

        public async Task UpdateDataAsync(UpdateProductDTO data)
        {
            var productToUpdate = await _repo.GetProductByNameAsync(n => n.ProductName == data.ProductName);

            _mapper.Map(data, productToUpdate);

            await _repo.SaveChangesAsync();
        }
    }
}

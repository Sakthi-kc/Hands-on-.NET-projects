using AutoMapper;
using Product_Management.Data.Repository;
using Product_Management.DTOs;
using Product_Management.Entity_Models;
using System.Linq.Expressions;

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

        public Task AddDataAsync(CreateProductDTO data)
        {
            throw new NotImplementedException();
        }

        public Task<List<ProductDTO>?> GetActiveProducts()
        {
            throw new NotImplementedException();
        }

        public Task<List<ProductDTO>> GetAllDataAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ProductDTO?> GetDataAsync(Expression<Func<ProductDTO, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public Task<ProductDTO?> GetDataByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<ProductDTO>?> GetProductsByCategory(string categoryName)
        {
            throw new NotImplementedException();
        }

        public void UpdateData(UpdateProductDTO data)
        {
            throw new NotImplementedException();
        }
    }
}

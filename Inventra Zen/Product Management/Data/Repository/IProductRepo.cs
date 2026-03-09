using Product_Management.Entity_Models;

namespace Product_Management.Data.Repository
{
    public interface IProductRepo : IZenRepo<ProductEntityModel>
    {
        Task<List<ProductEntityModel>?> GetActiveProducts();

        Task<List<ProductEntityModel>?> GetProductsByCategory(string categoryName);
    }
}

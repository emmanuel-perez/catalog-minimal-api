
using MinimalCatalogApi.Models;

namespace MinimalCatalogApi.Contracts {
    public interface IProductRepository {
        Task<IEnumerable<Product>> GetProducts();
        Task<Product> GetProductById(int productId);
        Task<bool> CreateProduct(UpsertProductDto productToCreate);
        Task<bool> UpdateProductById(int productId, UpsertProductDto productToUpdate);
        Task<bool> DeactivateProductById(int productId);
    }
}

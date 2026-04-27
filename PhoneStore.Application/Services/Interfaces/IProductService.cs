using PhoneStore.Application.DTOs.Product;

namespace PhoneStore.Application.Services.Interfaces
{
    public interface IProductService
    {
        Task CreateProduct(CreateProductDto model);
        Task UpdateProduct(Guid id, UpdateProductDto model);
        Task DeleteProduct(Guid id);
        Task<List<ProductDto>> GetAllProducts(int pageNumber = 1);
        Task<List<ProductDto>> GetProductsByCategory(Guid categoryId, int pageNumber = 1);
    }
}

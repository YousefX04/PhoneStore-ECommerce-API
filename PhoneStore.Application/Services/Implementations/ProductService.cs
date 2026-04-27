using FluentValidation;
using PhoneStore.Application.DTOs.Product;
using PhoneStore.Application.Services.Interfaces;
using PhoneStore.Domain.Entities;
using PhoneStore.Domain.Repositories.Interfaces;

namespace PhoneStore.Application.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<CreateProductDto> _createProductValidator;
        private readonly IValidator<UpdateProductDto> _updateProductValidator;

        public ProductService(IUnitOfWork unitOfWork, IValidator<CreateProductDto> createProductValidator, IValidator<UpdateProductDto> updateProductValidator)
        {
            _unitOfWork = unitOfWork;
            _createProductValidator = createProductValidator;
            _updateProductValidator = updateProductValidator;
        }

        public async Task CreateProduct(CreateProductDto model)
        {
            var result = _createProductValidator.Validate(model);

            if (!result.IsValid)
                throw new Exception(result.ToString(","));

            var product = new Product
            {
                Name = model.Name,
                Description = model.Description,
                RAM = model.RAM,
                CPU = model.CPU,
                Storage = model.Storage,
                Battery = model.Battery,
                Price = model.Price,
                StockQuantity = model.StockQuantity,
                CategoryId = model.CategoryId
            };

            await _unitOfWork.Products.AddAsync(product);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteProduct(Guid id)
        {
            var product = await _unitOfWork.Products
                .GetAsync(filter: p => p.Id == id);

            if (product == null)
                throw new Exception("Product not found");

            await _unitOfWork.Products.Delete(product);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<List<ProductDto>> GetAllProducts(int pageNumber = 1)
        {
            var products = await _unitOfWork.Products
                .GetAllAsync(
                filter: p => true,
                selector: p => new ProductDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    RAM = p.RAM,
                    CPU = p.CPU,
                    Storage = p.Storage,
                    Battery = p.Battery,
                    Price = p.Price,
                    StockQuantity = p.StockQuantity,
                    CategoryName = p.Category.Name
                },
                pageNumber: pageNumber,
                pageSize: 20
                );

            if (products == null)
                throw new Exception("No products found");

            return products;
        }

        public async Task<List<ProductDto>> GetProductsByCategory(Guid categoryId, int pageNumber = 1)
        {
            var products = await _unitOfWork.Products
                .GetAllAsync(
                filter: p => p.CategoryId == categoryId,
                selector: p => new ProductDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    RAM = p.RAM,
                    CPU = p.CPU,
                    Storage = p.Storage,
                    Battery = p.Battery,
                    Price = p.Price,
                    StockQuantity = p.StockQuantity,
                    CategoryName = p.Category.Name
                },
                pageNumber: pageNumber,
                pageSize: 20
                );

            if (products == null)
                throw new Exception("No products found");

            return products;
        }

        public async Task UpdateProduct(Guid id, UpdateProductDto model)
        {
            var result = _updateProductValidator.Validate(model);

            if (!result.IsValid)
                throw new Exception(result.ToString(","));

            var product = await _unitOfWork.Products
                .GetAsync(filter: p => p.Id == id);

            if (product == null)
                throw new Exception("Product not found");

            product.Name = model.Name;
            product.Description = model.Description;
            product.RAM = model.RAM;
            product.CPU = model.CPU;
            product.Storage = model.Storage;
            product.Battery = model.Battery;
            product.Price = model.Price;
            product.StockQuantity = model.StockQuantity;
            product.CategoryId = model.CategoryId;

            await _unitOfWork.SaveChangesAsync();
        }
    }
}

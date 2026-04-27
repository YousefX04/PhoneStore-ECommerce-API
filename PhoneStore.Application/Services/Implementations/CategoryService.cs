using FluentValidation;
using PhoneStore.Application.DTOs.Category;
using PhoneStore.Application.Services.Interfaces;
using PhoneStore.Domain.Entities;
using PhoneStore.Domain.Repositories.Interfaces;

namespace PhoneStore.Application.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<CreateCategoryDto> _createCategoryValidator;
        private readonly IValidator<UpdateCategoryDto> _updateCategoryValidator;

        public CategoryService(IUnitOfWork unitOfWork, IValidator<CreateCategoryDto> createCategoryValidator, IValidator<UpdateCategoryDto> updateCategoryValidator)
        {
            _unitOfWork = unitOfWork;
            _createCategoryValidator = createCategoryValidator;
            _updateCategoryValidator = updateCategoryValidator;
        }

        public async Task CreateCategory(CreateCategoryDto model)
        {
            var result = _createCategoryValidator.Validate(model);

            if (!result.IsValid)
                throw new Exception(result.ToString(","));

            var nameExists = await _unitOfWork.Categories
                .GetAsync(
                filter: s => s.Name == model.Name
                );

            if (nameExists != null)
                throw new Exception("A category with the same name already exists");

            var category = new Category
            {
                Name = model.Name,
            };

            await _unitOfWork.Categories.AddAsync(category);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteCategory(Guid id)
        {
            var category = await _unitOfWork.Categories
                .GetAsync(filter: c => c.Id == id);

            if (category == null)
                throw new Exception("Category not found");

            await _unitOfWork.Categories.Delete(category);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<List<CategoryDto>> GetAllCategories()
        {
            var categories = await _unitOfWork.Categories
                .GetAllAsync(
                filter: c => true,
                selector: c => new CategoryDto
                {
                    Id = c.Id,
                    Name = c.Name,
                });

            if (categories == null)
                throw new Exception("No categories found");

            return categories;
        }

        public async Task UpdateCategory(Guid id, UpdateCategoryDto model)
        {
            var result = _updateCategoryValidator.Validate(model);

            if (!result.IsValid)
                throw new Exception(result.ToString(","));

            var nameExists = await _unitOfWork.Categories
                .GetAsync(
                filter: s => s.Name == model.Name
                );

            if (nameExists != null)
                throw new Exception("A category with the same name already exists");

            var category = await _unitOfWork.Categories
                .GetAsync(filter: c => c.Id == id);

            category.Name = model.Name;

            await _unitOfWork.SaveChangesAsync();
        }
    }
}

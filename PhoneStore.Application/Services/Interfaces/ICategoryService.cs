using PhoneStore.Application.DTOs.Category;

namespace PhoneStore.Application.Services.Interfaces
{
    public interface ICategoryService
    {
        Task CreateCategory(CreateCategoryDto model);
        Task UpdateCategory(Guid id, UpdateCategoryDto model);
        Task DeleteCategory(Guid id);
        Task<List<CategoryDto>> GetAllCategories();
    }
}

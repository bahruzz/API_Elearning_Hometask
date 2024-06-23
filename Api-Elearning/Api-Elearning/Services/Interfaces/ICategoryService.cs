using Api_Elearning.DTOs.Categories;
using Api_Elearning.Models;

namespace Api_Elearning.Services.Interfaces
{
    public interface ICategoryService
    {
        Task CreateAsync(CategoryCreateDto request);
        Task EditAsync(Category category, CategoryEditDto request);
        Task DeleteAsync(Category category);
        Task<IEnumerable<Category>> GetAllAsync();     
        Task<Category> GetByIdAsync(int id);
        Task<bool> ExistAsync(string name);
    }
}

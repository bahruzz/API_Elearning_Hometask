using Api_Elearning.DTOs.Abouts;
using Api_Elearning.DTOs.Sliders;
using Api_Elearning.Models;

namespace Api_Elearning.Services.Interfaces
{
    public interface IAboutService
    {
        Task CreateAsync(AboutCreateDto request);
        Task EditAsync(About about, AboutEditDto request);
        Task DeleteAsync(About about);
        Task<IEnumerable<About>> GetAllAsync();
        Task<About> GetByIdAsync(int id);
        Task<bool> ExistAsync(string title);
    }
}

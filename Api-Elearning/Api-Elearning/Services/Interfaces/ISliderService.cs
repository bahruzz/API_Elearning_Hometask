using Api_Elearning.DTOs.Sliders;
using Api_Elearning.Models;
using System.Reflection.Metadata;

namespace Api_Elearning.Services.Interfaces
{
    public interface ISliderService
    {
        Task CreateAsync(SliderCreateDto request);
        Task EditAsync(Slider slider, SliderEditDto request);
        Task DeleteAsync(Slider slider);
        Task<IEnumerable<Slider>> GetAllAsync();
        Task<Slider> GetByIdAsync(int id);
        Task<bool> ExistAsync(string title);
    }
}

using Api_Elearning.DTOs.Abouts;
using Api_Elearning.DTOs.Informations;
using Api_Elearning.Models;
using Microsoft.VisualBasic;
using Information = Api_Elearning.Models.Information;

namespace Api_Elearning.Services.Interfaces
{
    public interface IInformationService
    {
        Task CreateAsync(InformtationCreateDto request);
        Task EditAsync(Information information, InformationEditDto request);
        Task DeleteAsync(Information information);
        Task<IEnumerable<Information>> GetAllAsync();
        Task<Information> GetByIdAsync(int id);
        Task<bool> ExistAsync(string title);
    }
}

using Api_Elearning.DTOs.Abouts;
using Api_Elearning.DTOs.Instructors;
using Api_Elearning.Models;

namespace Api_Elearning.Services.Interfaces
{
    public interface IInstructorService
    {
        Task<bool> ExistEmailAsync(string email);
        Task<bool> ExistPhoneAsync(string phone);
        Task CreateAsync(InstructorCreateDto request);
        Task<IEnumerable<Instructor>> GetAllAsync();
        Task<Instructor> GetByIdAsync(int id);
        Task EditAsync(Instructor instructor, InstructorEditDto request);
        Task DeleteAsync(Instructor instructor);
    }
}

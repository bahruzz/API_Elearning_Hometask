using Api_Elearning.DTOs.Courses;
using Api_Elearning.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Api_Elearning.Services.Interfaces
{
    public interface ICourseService
    {
        Task CreateAsync(CourseCreateDto request);
        Task DeleteAsync(Course course);
        Task EditAsync(Course course, CourseEditDto request);
      
        Task<IEnumerable<Course>> GetAllPopularAsync();
      
        Task<SelectList> GetAllSelectedAvailableAsync(int studentId);
       
        Task<Course> GetByIdAsync(int id);
        Task<Course> GetByIdWithImagesAsync(int id);
        Task<Course> GetByIdWithAllDatasAsync(int id);
        Task<int> GetCountAsync();
        Task<bool> ExistAsync(string name);
        Task DeleteCourseImageAsync(DeleteAndMainDto request);
        Task SetMainImageAsync(DeleteAndMainDto request);
    }
}

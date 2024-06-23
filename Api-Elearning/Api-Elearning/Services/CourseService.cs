using Api_Elearning.Data;
using Api_Elearning.DTOs.Courses;
using Api_Elearning.Helpers.Extensions;
using Api_Elearning.Models;
using Api_Elearning.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Api_Elearning.Services
{
    public class CourseService : ICourseService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IMapper _mapper;
        public CourseService( IMapper mapper,
                              AppDbContext context,
                              IWebHostEnvironment env)
        {
            _mapper = mapper;
            _context = context;
            _env = env;
        }
        public async Task CreateAsync(CourseCreateDto request)
        {
            List<CourseImage> images = new();

            foreach (var item in request.Images)
            {
                string fileName = $"{Guid.NewGuid()}-{item.FileName}";

                string path = _env.GenerateFilePath("img", fileName);

                await item.SaveFileToLocalAsync(path);

                images.Add(new CourseImage { Name = fileName });

                request.Image = fileName;
            }
            images.FirstOrDefault().IsMain = true;

            await _context.Courses.AddAsync(_mapper.Map<Course>(request));
            await _context.SaveChangesAsync();
        }

        public Task DeleteAsync(Course course)
        {
            throw new NotImplementedException();
        }

        public Task DeleteCourseImageAsync(DeleteAndMainDto request)
        {
            throw new NotImplementedException();
        }

        public Task EditAsync(Course course, CourseEditDto request)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ExistAsync(string name)
        {
            return await _context.Courses.AnyAsync(m => m.Name.Trim().ToLower() == name.Trim().ToLower());
        }

        public async Task<IEnumerable<Course>> GetAllPopularAsync()
        {
            return await _context.Courses             
                .Include(m => m.CourseImages)
                .Include(m => m.Category)
                .Include(m => m.Instructor)
                .Include(m => m.CourseStudents)
                .ToListAsync();
        }

        public Task<SelectList> GetAllSelectedAvailableAsync(int studentId)
        {
            throw new NotImplementedException();
        }

        public Task<Course> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Course> GetByIdWithAllDatasAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Course> GetByIdWithImagesAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetCountAsync()
        {
            throw new NotImplementedException();
        }

        public Task SetMainImageAsync(DeleteAndMainDto request)
        {
            throw new NotImplementedException();
        }
    }
}

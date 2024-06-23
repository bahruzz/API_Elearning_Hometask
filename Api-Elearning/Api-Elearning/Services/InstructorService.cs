using Api_Elearning.Data;
using Api_Elearning.DTOs.Abouts;
using Api_Elearning.DTOs.Instructors;
using Api_Elearning.Helpers.Extensions;
using Api_Elearning.Models;
using Api_Elearning.Services.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Api_Elearning.Services
{
    public class InstructorService : IInstructorService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IMapper _mapper;

        public InstructorService(IMapper mapper,
                                 AppDbContext context,
                                 IWebHostEnvironment env)
        {
            _mapper = mapper;
            _context = context;
            _env = env;
        }
        public async Task CreateAsync(InstructorCreateDto request)
        {
            string fileName = $"{Guid.NewGuid()}-{request.UploadImage.FileName}";

            string path = _env.GenerateFilePath("img", fileName);
            await request.UploadImage.SaveFileToLocalAsync(path);
            request.Image = fileName;

            await _context.Instructors.AddAsync(_mapper.Map<Instructor>(request));



            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Instructor instructor)
        {
            string imagePath = _env.GenerateFilePath("img", instructor.Image);
            imagePath.DeleteFileFromLocal();

            _context.Instructors.Remove(instructor);
            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(Instructor instructor, InstructorEditDto request)
        {

            if (request.NewImage is not null)
            {
                string oldPath = _env.GenerateFilePath("img", instructor.Image);

                oldPath.DeleteFileFromLocal();

                string fileName = $"{Guid.NewGuid()}-{request.NewImage.FileName}";

                string newPath = _env.GenerateFilePath("img", fileName);

                await request.NewImage.SaveFileToLocalAsync(newPath);

                request.Image = fileName;
            }

            _mapper.Map(request, instructor);
            _context.Instructors.Update(instructor);

            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistEmailAsync(string email)
        {
            return await _context.Instructors.AnyAsync(m => m.Email.Trim() == email.Trim());
        }

        public async Task<bool> ExistPhoneAsync(string phone)
        {
            return await _context.Instructors.AnyAsync(m => m.Phone.Trim().ToLower() == phone.Trim().ToLower());
        }

        public async Task<IEnumerable<Instructor>> GetAllAsync()
        {
            return await _context.Instructors.AsNoTracking().ToListAsync();
        }

        public async Task<Instructor> GetByIdAsync(int id)
        {
            return await _context.Instructors.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);
        }
    }
}

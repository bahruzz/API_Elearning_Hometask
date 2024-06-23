using Api_Elearning.Data;
using Api_Elearning.DTOs.Abouts;
using Api_Elearning.DTOs.Sliders;
using Api_Elearning.Helpers.Extensions;
using Api_Elearning.Models;
using Api_Elearning.Services.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Api_Elearning.Services
{
    public class AboutService : IAboutService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IMapper _mapper;
        public AboutService(
                            AppDbContext context,
                            IWebHostEnvironment env,
                            IMapper mapper)
        {
            _context = context;
            _env = env;
            _mapper = mapper;
        }

        public async Task CreateAsync(AboutCreateDto request)
        {
            string fileName = $"{Guid.NewGuid()}-{request.UploadImage.FileName}";

            string path = _env.GenerateFilePath("img", fileName);
            await request.UploadImage.SaveFileToLocalAsync(path);
            request.Image = fileName;

            await _context.Abouts.AddAsync(_mapper.Map<About>(request));



            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(About about)
        {
            string imagePath = _env.GenerateFilePath("img", about.Image);
            imagePath.DeleteFileFromLocal();

            _context.Abouts.Remove(about);
            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(About about, AboutEditDto request)
        {
            if (request.NewImage is not null)
            {
                string oldPath = _env.GenerateFilePath("img", about.Image);

                oldPath.DeleteFileFromLocal();

                string fileName = $"{Guid.NewGuid()}-{request.NewImage.FileName}";

                string newPath = _env.GenerateFilePath("img", fileName);

                await request.NewImage.SaveFileToLocalAsync(newPath);

                request.Image = fileName;
            }

            _mapper.Map(request, about);
            _context.Abouts.Update(about);

            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistAsync(string title)
        {
            return await _context.Abouts.AnyAsync(m => m.Title.Trim() == title.Trim());
        }

        public async Task<IEnumerable<About>> GetAllAsync()
        {
            return await _context.Abouts.AsNoTracking().ToListAsync();
        }

        public async Task<About> GetByIdAsync(int id)
        {
            return await _context.Abouts.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);
        }
    }
}

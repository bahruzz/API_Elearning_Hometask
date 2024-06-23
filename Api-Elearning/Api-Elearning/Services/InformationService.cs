using Api_Elearning.Data;
using Api_Elearning.DTOs.Abouts;
using Api_Elearning.DTOs.Informations;
using Api_Elearning.Helpers.Extensions;
using Api_Elearning.Models;
using Api_Elearning.Services.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Information = Api_Elearning.Models.Information;

namespace Api_Elearning.Services
{
    public class InformationService : IInformationService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IMapper _mapper;
        public InformationService(
                            AppDbContext context,
                            IWebHostEnvironment env,
                            IMapper mapper)
        {
            _context = context;
            _env = env;
            _mapper = mapper;
        }

        public async Task CreateAsync(InformtationCreateDto request)
        {
            string fileName = $"{Guid.NewGuid()}-{request.UploadImage.FileName}";

            string path = _env.GenerateFilePath("img", fileName);
            await request.UploadImage.SaveFileToLocalAsync(path);
            request.Image = fileName;

            await _context.Informations.AddAsync(_mapper.Map<Information>(request));



            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Information  information)
        {
            string imagePath = _env.GenerateFilePath("img", information.Image);
            imagePath.DeleteFileFromLocal();

            _context.Informations.Remove(information);
            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(Information information, InformationEditDto request)
        {
            if (request.NewImage is not null)
            {
                string oldPath = _env.GenerateFilePath("img", information.Image);

                oldPath.DeleteFileFromLocal();

                string fileName = $"{Guid.NewGuid()}-{request.NewImage.FileName}";

                string newPath = _env.GenerateFilePath("img", fileName);

                await request.NewImage.SaveFileToLocalAsync(newPath);

                request.Image = fileName;
            }

            _mapper.Map(request, information);
            _context.Informations.Update(information);

            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistAsync(string title)
        {
            return await _context.Informations.AnyAsync(m => m.Title.Trim() == title.Trim());
        }

        public async Task<IEnumerable<Information>> GetAllAsync()
        {
            return await _context.Informations.AsNoTracking().ToListAsync();
        }

        public async Task<Information> GetByIdAsync(int id)
        {
            return await _context.Informations.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);
        }
    }
}

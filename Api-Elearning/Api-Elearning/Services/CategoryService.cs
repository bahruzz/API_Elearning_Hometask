using Api_Elearning.Data;
using Api_Elearning.DTOs.Categories;
using Api_Elearning.Helpers.Extensions;
using Api_Elearning.Models;
using Api_Elearning.Services.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Api_Elearning.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IMapper _mapper;
        public CategoryService(
                            AppDbContext context,
                            IWebHostEnvironment env,
                            IMapper mapper)
        {
            _context = context;
            _env = env;
            _mapper = mapper;
        }
        public async Task CreateAsync(CategoryCreateDto request)
        {
            string fileName = $"{Guid.NewGuid()}-{request.UploadImage.FileName}";

            string path = _env.GenerateFilePath("img", fileName);
            await request.UploadImage.SaveFileToLocalAsync(path);
            request.Image = fileName;

            await _context.Categories.AddAsync(_mapper.Map<Category>(request));

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Category category)
        {
            string imagePath = _env.GenerateFilePath("img", category.Image);
            imagePath.DeleteFileFromLocal();

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(Category category, CategoryEditDto request)
        {
            if (request.NewImage is not null)
            {
                string oldPath = _env.GenerateFilePath("img", category.Image);

                oldPath.DeleteFileFromLocal();

                string fileName = $"{Guid.NewGuid()}-{request.NewImage.FileName}";

                string newPath = _env.GenerateFilePath("img", fileName);

                await request.NewImage.SaveFileToLocalAsync(newPath);

                request.Image = fileName;
            }

            _mapper.Map(request, category);
            _context.Categories.Update(category);

            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistAsync(string name)
        {
            return await _context.Categories.AnyAsync(m => m.Name.Trim() == name.Trim());
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _context.Categories.AsNoTracking().ToListAsync();
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            return await _context.Categories.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);
        }
    }
}

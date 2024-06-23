using Api_Elearning.DTOs.Abouts;
using Api_Elearning.DTOs.Categories;
using Api_Elearning.Helpers.Extensions;
using Api_Elearning.Services;
using Api_Elearning.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api_Elearning.Controllers
{

    public class CategoryController : BaseController
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryService categoryService,
                                  IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(_mapper.Map<IEnumerable<CategoryDto>>(await _categoryService.GetAllAsync()));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CategoryCreateDto request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (await _categoryService.ExistAsync(request.Name))
            {
                ModelState.AddModelError("Name", "This category already exist");
                return BadRequest(ModelState);
            }

            if (!request.UploadImage.CheckFileType("image/"))
            {
                ModelState.AddModelError("UploadImage", "Input can accept only image format");
                return BadRequest(ModelState);
            }

            if (!request.UploadImage.CheckFileSize(500))
            {
                ModelState.AddModelError("UploadImage", "Image size must be max 500 KB");
                return BadRequest(ModelState);
            }

            await _categoryService.CreateAsync(request);

            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var entity = await _categoryService.GetByIdAsync(id);
            if (entity == null) return NotFound();
            return Ok(_mapper.Map<CategoryDto>(entity));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            if (id == null) return BadRequest();

            var category = await _categoryService.GetByIdAsync(id);

            if (category is null) return NotFound();

            await _categoryService.DeleteAsync(category);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit([FromRoute] int id, [FromForm] CategoryEditDto request)
        {
            var category = await _categoryService.GetByIdAsync(id);

            if (category is null) return NotFound();

            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (request.NewImage is not null)
            {
                if (!request.NewImage.CheckFileType("image/"))
                {
                    ModelState.AddModelError("NewImage", "Input can accept only image format");
                    return BadRequest(ModelState);
                }

                if (!request.NewImage.CheckFileSize(500))
                {
                    ModelState.AddModelError("NewImage", "Image size must be max 500 KB");
                    return BadRequest(ModelState);
                }
            }

            if (category.Name.Trim().ToLower() != request.Name.Trim().ToLower() && await _categoryService.ExistAsync(request.Name))
            {
                ModelState.AddModelError("Name", "This category already exist");
                return BadRequest(ModelState);
            }

            await _categoryService.EditAsync(category, request);

            return Ok();

        }
    }
}

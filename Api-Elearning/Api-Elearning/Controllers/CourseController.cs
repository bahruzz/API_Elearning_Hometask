using Api_Elearning.DTOs.Abouts;
using Api_Elearning.DTOs.Courses;
using Api_Elearning.Helpers.Extensions;
using Api_Elearning.Services;
using Api_Elearning.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api_Elearning.Controllers
{
   
    public class CourseController : BaseController
    {
        private readonly ICourseService _courseService;
        private readonly ICategoryService _categoryService;
        private readonly IInstructorService _instructorService;
        private readonly IMapper _mapper;

        public CourseController(IMapper mapper,
                                ICourseService courseService,
                                ICategoryService categoryService,
                                IInstructorService instructorService)
        {
            _mapper = mapper;
            _courseService = courseService;
            _categoryService = categoryService;
            _instructorService = instructorService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(_mapper.Map<IEnumerable<CourseDto>>(await _courseService.GetAllPopularAsync()));
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CourseCreateDto request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (await _courseService.ExistAsync(request.Name))
            {
                ModelState.AddModelError("Name", "Course with this name already exists");

                return BadRequest(ModelState);
            }

            foreach (var item in request.Images)
            {
                if (!item.CheckFileSize(500))
                {
                    ModelState.AddModelError("Images", "Image size can be max 500 Kb");

                    return BadRequest(ModelState);
                }

                if (!item.CheckFileType("image/"))
                {
                    ModelState.AddModelError("Images", "File type must be only image");
                   
                  return BadRequest(ModelState);
                }

            }


            await _courseService.CreateAsync(request);

            return Ok();
        }
    }
}

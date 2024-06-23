using Api_Elearning.DTOs.Abouts;
using Api_Elearning.DTOs.Sliders;
using Api_Elearning.Helpers.Extensions;
using Api_Elearning.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api_Elearning.Controllers
{
   
    public class AboutController : BaseController
    {
        private readonly IAboutService _aboutService;
        private readonly IMapper _mapper;

        public AboutController(
            IAboutService aboutService,
            IMapper mapper)
        {
            _aboutService = aboutService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(_mapper.Map<IEnumerable<AboutDto>>(await _aboutService.GetAllAsync()));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] AboutCreateDto request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (await _aboutService.ExistAsync(request.Title))
            {
                ModelState.AddModelError("Title", "This title already exist");
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

            await _aboutService.CreateAsync(request);

            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var entity = await _aboutService.GetByIdAsync(id);
            if (entity == null) return NotFound();
            return Ok(_mapper.Map<AboutDto>(entity));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            if (id == null) return BadRequest();

            var about = await _aboutService.GetByIdAsync(id);

            if (about is null) return NotFound();

            await _aboutService.DeleteAsync(about);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit([FromRoute] int id, [FromForm] AboutEditDto request)
        {
            var about = await _aboutService.GetByIdAsync(id);

            if (about is null) return NotFound();

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

            if (about.Title.Trim().ToLower() != request.Title.Trim().ToLower() && await _aboutService.ExistAsync(request.Title))
            {
                ModelState.AddModelError("Title", "This title already exist");
                return BadRequest(ModelState);
            }

            await _aboutService.EditAsync(about, request);

            return Ok();

        }

    }
}

using Api_Elearning.DTOs.Sliders;
using Api_Elearning.Helpers.Extensions;
using Api_Elearning.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api_Elearning.Controllers
{
   
    public class SliderController : BaseController
    {
        private readonly ISliderService _sliderService;
        private readonly IMapper _mapper;

        public SliderController(
            ISliderService sliderService,
            IMapper mapper)
        {
            _sliderService = sliderService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(_mapper.Map<IEnumerable<SliderDto>>(await _sliderService.GetAllAsync()));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] SliderCreateDto request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (await _sliderService.ExistAsync(request.Title))
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

            await _sliderService.CreateAsync(request);

            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var entity = await _sliderService.GetByIdAsync(id);
            if (entity == null) return NotFound();
            return Ok(_mapper.Map<SliderDto>(entity));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            if (id == null) return BadRequest();

            var slider = await _sliderService.GetByIdAsync(id);

            if (slider is null) return NotFound();

            await _sliderService.DeleteAsync(slider);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit([FromRoute] int id, [FromForm] SliderEditDto request)
        {
            var slider = await _sliderService.GetByIdAsync(id);

            if (slider is null) return NotFound();

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

            if (slider.Title.Trim().ToLower() != request.Title.Trim().ToLower() && await _sliderService.ExistAsync(request.Title))
            {
                ModelState.AddModelError("Title", "This blog already exist");
                return BadRequest(ModelState);
            }

            await _sliderService.EditAsync(slider, request);

            return Ok();

        }


    }

}

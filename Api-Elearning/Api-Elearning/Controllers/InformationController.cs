using Api_Elearning.DTOs.Abouts;
using Api_Elearning.DTOs.Informations;
using Api_Elearning.Helpers.Extensions;
using Api_Elearning.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api_Elearning.Controllers
{

    public class InformationController : BaseController
    {
        private readonly IInformationService _informationService;
        private readonly IMapper _mapper;

        public InformationController(
                                     IInformationService informationService,
                                     IMapper mapper)
        {
            _informationService = informationService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(_mapper.Map<IEnumerable<InformationDto>>(await _informationService.GetAllAsync()));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] InformtationCreateDto request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (await _informationService.ExistAsync(request.Title))
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

            await _informationService.CreateAsync(request);

            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var entity = await _informationService.GetByIdAsync(id);
            if (entity == null) return NotFound();
            return Ok(_mapper.Map<InformationDto>(entity));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            if (id == null) return BadRequest();

            var information = await _informationService.GetByIdAsync(id);

            if (information is null) return NotFound();

            await _informationService.DeleteAsync(information);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit([FromRoute] int id, [FromForm] InformationEditDto request)
        {
            var info = await _informationService.GetByIdAsync(id);

            if (info is null) return NotFound();

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

            if (info.Title.Trim().ToLower() != request.Title.Trim().ToLower() && await _informationService.ExistAsync(request.Title))
            {
                ModelState.AddModelError("Title", "This title already exist");
                return BadRequest(ModelState);
            }

            await _informationService.EditAsync(info, request);

            return Ok();

        }
    }
}

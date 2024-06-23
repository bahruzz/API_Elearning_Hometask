using Api_Elearning.DTOs.Abouts;
using Api_Elearning.DTOs.Instructors;
using Api_Elearning.Helpers.Extensions;
using Api_Elearning.Models;
using Api_Elearning.Services;
using Api_Elearning.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api_Elearning.Controllers
{
 
    public class InstructorController : BaseController
    {
        private readonly IInstructorService _instructorService;
        private readonly ISocialService _socialService;
        private readonly IMapper _mapper;

        public InstructorController(IMapper mapper,
                                    IInstructorService instructorService,
                                    ISocialService socialService)
        {
            _mapper = mapper;
            _instructorService = instructorService;
            _socialService = socialService;
        } 


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(_mapper.Map<IEnumerable<InstructorDto>>(await _instructorService.GetAllAsync()));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] InstructorCreateDto request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (await _instructorService.ExistEmailAsync(request.Email))
            {
                ModelState.AddModelError("Email", "Instructor with this email already exists");
                return BadRequest(ModelState);
            }
            if (await _instructorService.ExistPhoneAsync(request.Phone))
            {
                ModelState.AddModelError("Phone", "Instructor with this phone already exists");
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

            await _instructorService.CreateAsync(request);

            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var entity = await _instructorService.GetByIdAsync(id);
            if (entity == null) return NotFound();
            return Ok(_mapper.Map<InstructorDto>(entity));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            if (id == null) return BadRequest();

            var entity = await _instructorService.GetByIdAsync(id);

            if (entity is null) return NotFound();

            await _instructorService.DeleteAsync(entity);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit([FromRoute] int id, [FromForm] InstructorEditDto request)
        {
            var instructor = await _instructorService.GetByIdAsync(id);

            if (instructor is null) return NotFound();

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

            if (instructor.Email.Trim().ToLower() != request.Email.Trim().ToLower() && await _instructorService.ExistEmailAsync(request.Email))
            {
                ModelState.AddModelError("Email", "This email already exist");
                return BadRequest(ModelState);
            }
            if (instructor.Phone.Trim().ToLower() != request.Phone.Trim().ToLower() && await _instructorService.ExistPhoneAsync(request.Phone))
            {
                ModelState.AddModelError("Phone", "This Phone already exist");
                return BadRequest(ModelState);
            }
            await _instructorService.EditAsync(instructor, request);

            return Ok();

        }
    }
}

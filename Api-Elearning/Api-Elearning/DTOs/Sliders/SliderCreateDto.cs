﻿using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace Api_Elearning.DTOs.Sliders
{
    public class SliderCreateDto
    {
       
        public string Title { get; set; }
       
        public string Description { get; set; }

        [SwaggerSchema(ReadOnly=true)]
        public string? Image { get; set; }

        public IFormFile UploadImage { get; set; }
    }
}

using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace Api_Elearning.DTOs.Categories
{
    public class CategoryCreateDto
    {
        public string Name { get; set; }

        [SwaggerSchema(ReadOnly = true)]
        public string? Image { get; set; }

        public IFormFile UploadImage { get; set; }
    }
}

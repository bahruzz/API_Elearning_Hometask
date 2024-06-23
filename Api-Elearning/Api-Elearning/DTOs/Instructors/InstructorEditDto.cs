using Swashbuckle.AspNetCore.Annotations;

namespace Api_Elearning.DTOs.Instructors
{
    public class InstructorEditDto
    {
        public string FullName { get; set; }
        public string Field { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }

        [SwaggerSchema(ReadOnly = true)]
        public string? Image { get; set; }
        public IFormFile NewImage { get; set; }
    }
}

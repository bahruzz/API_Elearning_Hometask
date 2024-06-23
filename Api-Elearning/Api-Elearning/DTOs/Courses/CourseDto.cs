using Swashbuckle.AspNetCore.Annotations;

namespace Api_Elearning.DTOs.Courses
{
    public class CourseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Instructor { get; set; }
        public string Category { get; set; }

        [SwaggerSchema(ReadOnly = true)]
        public List<CourseImageDto>? Images { get; set; }
        public string MainImage { get; set; }
    }
}

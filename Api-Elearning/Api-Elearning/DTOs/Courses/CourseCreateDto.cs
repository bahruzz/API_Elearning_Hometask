using System.ComponentModel.DataAnnotations;

namespace Api_Elearning.DTOs.Courses
{
    public class CourseCreateDto
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Price { get; set; }
        public int Rating { get; set; }
        public int? CategoryId { get; set; }
        public int? InstructorId { get; set; }

        public string Image { get; set; }
       
        public List<IFormFile> Images { get; set; }
    }
}

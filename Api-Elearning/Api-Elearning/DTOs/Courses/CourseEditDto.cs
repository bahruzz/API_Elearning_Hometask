using System.ComponentModel.DataAnnotations;

namespace Api_Elearning.DTOs.Courses
{
    public class CourseEditDto
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Price { get; set; }
        public int CategoryId { get; set; }
        public int InstructorId { get; set; }
        public int Rating { get; set; }
        [Required(ErrorMessage = "Start date is required")]
      
        public List<CourseImageEditDto> Images { get; set; }
        public List<IFormFile> NewImages { get; set; }
    }
}

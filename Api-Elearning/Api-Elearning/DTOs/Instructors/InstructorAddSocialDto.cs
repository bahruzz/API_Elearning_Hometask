using System.ComponentModel.DataAnnotations;

namespace Api_Elearning.DTOs.Instructors
{
    public class InstructorAddSocialDto
    {
        public int? SocialId { get; set; }
        [Required]
        public string SocialLink { get; set; }
    }
}

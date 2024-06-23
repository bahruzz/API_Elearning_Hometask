using System.ComponentModel.DataAnnotations;

namespace Api_Elearning.DTOs.Sliders
{
    public class SliderEditDto
    {
       
        public string Title { get; set; }
        
        public string Description { get; set; }
        public string? Image { get; set; }
        public IFormFile? NewImage { get; set; }
    }
}

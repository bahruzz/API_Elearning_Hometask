namespace Api_Elearning.DTOs.Abouts
{
    public class AboutEditDto
    {
        public string Title { get; set; }

        public string Description { get; set; }
        public string? Image { get; set; }

        public IFormFile NewImage { get; set; }
    }
}

namespace Api_Elearning.DTOs.Categories
{
    public class CategoryEditDto
    {
        public string Name { get; set; }
        public string? Image { get; set; }
        public IFormFile NewImage { get; set; }
    }
}

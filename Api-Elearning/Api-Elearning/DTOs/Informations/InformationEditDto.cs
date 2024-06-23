namespace Api_Elearning.DTOs.Informations
{
    public class InformationEditDto
    {
        public string Title { get; set; }

        public string Description { get; set; }
        public string? Image { get; set; }

        public IFormFile NewImage { get; set; }
    }
}

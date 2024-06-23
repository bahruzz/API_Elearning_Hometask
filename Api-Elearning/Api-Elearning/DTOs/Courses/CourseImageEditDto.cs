namespace Api_Elearning.DTOs.Courses
{
    public class CourseImageEditDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsMain { get; set; }
        public int CourseId { get; set; }
    }
}

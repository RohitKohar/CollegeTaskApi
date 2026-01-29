namespace CollegeApi.DTOs
{
    public class SubjectDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class CreateSubjectDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}

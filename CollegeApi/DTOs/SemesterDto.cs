namespace CollegeApi.DTOs
{
    public class SemesterDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class CreateSemesterDto
    {
        public string Name { get; set; }
    }
}

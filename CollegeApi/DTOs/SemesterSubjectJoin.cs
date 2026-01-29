namespace CollegeApi.DTOs
{
    public class SemesterSubjectJoinDto
    {
        public int Id { get; set; }
        public int SemesterId { get; set; }
        public string SemesterName { get; set; }
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
    }

    public class CreateSemesterSubjectJoinDto
    {
        public int SemesterId { get; set; }
        public int SubjectId { get; set; }
    }
}

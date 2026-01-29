namespace CollegeApi.Models
{
    public class SemesterSubjectJoin
    {
        public int Id { get; set; }

        public int SemesterId { get; set; }
        public Semester Semester { get; set; }

        public int SubjectId { get; set; }
        public Subject Subject { get; set; }
    }
}

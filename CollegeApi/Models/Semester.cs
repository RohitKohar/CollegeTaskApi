namespace CollegeApi.Models
{
    public class Semester
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Student> Students { get; set; }
        public ICollection<SemesterSubjectJoin> SemesterSubjects { get; set; }
    }
}

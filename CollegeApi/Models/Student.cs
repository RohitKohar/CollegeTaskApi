namespace CollegeApi.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int SemesterId { get; set; }
        public Semester Semester { get; set; }
    }
}

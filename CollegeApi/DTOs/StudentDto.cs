namespace CollegeApi.DTOs
{
    public class StudentDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int SemesterId { get; set; }
        public string SemesterName { get; set; }
        public List<SubjectDto> Subjects { get; set; }
    }

    public class CreateStudentDto
    {
        public string Name { get; set; }
        public int SemesterId { get; set; }
    }
}

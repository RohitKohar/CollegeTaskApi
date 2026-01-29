using CollegeApi.Data;
using CollegeApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CollegeApi.DTOs;

namespace CollegeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly AppDbContext _context;
        public StudentsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentDto>>> GetStudents()
        {
            var students = await _context.Students
                .Include(s => s.Semester)
                    .ThenInclude(sem => sem.SemesterSubjects)
                        .ThenInclude(ss => ss.Subject)
                .ToListAsync();

            var studentDtos = students.Select(student => new StudentDto
            {
                Id = student.Id,
                Name = student.Name,
                SemesterId = student.SemesterId,
                SemesterName = student.Semester.Name,
                Subjects = student.Semester.SemesterSubjects
                    .Select(ss => new SubjectDto
                    {
                        Id = ss.Subject.Id,
                        Name = ss.Subject.Name,
                        Description = ss.Subject.Description
                    }).ToList()
            }).ToList();

            return studentDtos;
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<StudentDto>> GetStudent(int id)
        {
            var student = await _context.Students
                .Include(s => s.Semester)
                    .ThenInclude(sem => sem.SemesterSubjects)
                        .ThenInclude(ss => ss.Subject)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (student == null) return NotFound();

            var studentDto = new StudentDto
            {
                Id = student.Id,
                Name = student.Name,
                SemesterId = student.SemesterId,
                SemesterName = student.Semester.Name,
                Subjects = student.Semester.SemesterSubjects
                    .Select(ss => new SubjectDto
                    {
                        Id = ss.Subject.Id,
                        Name = ss.Subject.Name,
                        Description = ss.Subject.Description
                    }).ToList()
            };

            return studentDto;
        }


        [HttpPost]
        public async Task<ActionResult<StudentDto>> CreateStudent(CreateStudentDto dto)
        {
            var student = new Student
            {
                Name = dto.Name,
                SemesterId = dto.SemesterId
            };
            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetStudent), new { id = student.Id }, new StudentDto
            {
                Id = student.Id,
                Name = student.Name,
                SemesterId = student.SemesterId,
                SemesterName = (await _context.Semesters.FindAsync(student.SemesterId)).Name
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(int id, CreateStudentDto dto)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null) return NotFound();

            student.Name = dto.Name;
            student.SemesterId = dto.SemesterId;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null) return NotFound();

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }

}

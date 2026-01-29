using CollegeApi.Data;
using CollegeApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CollegeApi.DTOs;

namespace CollegeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectsController : ControllerBase
    {
        private readonly AppDbContext _context;
        public SubjectsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubjectDto>>> GetSubjects()
        {
            return await _context.Subjects
                .Select(s => new SubjectDto { Id = s.Id, Name = s.Name, Description = s.Description })
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SubjectDto>> GetSubject(int id)
        {
            var subject = await _context.Subjects.FindAsync(id);
            if (subject == null) return NotFound();

            return new SubjectDto { Id = subject.Id, Name = subject.Name, Description = subject.Description };
        }

        [HttpPost]
        public async Task<ActionResult<SubjectDto>> CreateSubject(CreateSubjectDto dto)
        {
            var subject = new Subject
            {
                Name = dto.Name,
                Description = dto.Description
            };
            _context.Subjects.Add(subject);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSubject), new { id = subject.Id }, new SubjectDto
            {
                Id = subject.Id,
                Name = subject.Name,
                Description = subject.Description
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSubject(int id, CreateSubjectDto dto)
        {
            var subject = await _context.Subjects.FindAsync(id);
            if (subject == null) return NotFound();

            subject.Name = dto.Name;
            subject.Description = dto.Description;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubject(int id)
        {
            var subject = await _context.Subjects.FindAsync(id);
            if (subject == null) return NotFound();

            _context.Subjects.Remove(subject);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }

}

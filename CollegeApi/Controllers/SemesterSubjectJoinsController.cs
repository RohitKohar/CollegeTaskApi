using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CollegeApi.Data;
using CollegeApi.DTOs;
using CollegeApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CollegeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SemesterSubjectJoinsController : ControllerBase
    {
        private readonly AppDbContext _context;
        public SemesterSubjectJoinsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SemesterSubjectJoinDto>>> GetSemesterSubjectJoins()
        {
            return await _context.SemesterSubjectJoins
                .Include(ss => ss.Semester)
                .Include(ss => ss.Subject)
                .Select(ss => new SemesterSubjectJoinDto
                {
                    Id = ss.Id,
                    SemesterId = ss.SemesterId,
                    SemesterName = ss.Semester.Name,
                    SubjectId = ss.SubjectId,
                    SubjectName = ss.Subject.Name
                })
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SemesterSubjectJoinDto>> GetSemesterSubjectJoin(int id)
        {
            var ss = await _context.SemesterSubjectJoins
                .Include(ss => ss.Semester)
                .Include(ss => ss.Subject)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (ss == null) return NotFound();

            return new SemesterSubjectJoinDto
            {
                Id = ss.Id,
                SemesterId = ss.SemesterId,
                SemesterName = ss.Semester.Name,
                SubjectId = ss.SubjectId,
                SubjectName = ss.Subject.Name
            };
        }

        [HttpPost]
        public async Task<ActionResult<SemesterSubjectJoinDto>> CreateSemesterSubjectJoin(CreateSemesterSubjectJoinDto dto)
        {
            var ss = new SemesterSubjectJoin
            {
                SemesterId = dto.SemesterId,
                SubjectId = dto.SubjectId
            };
            _context.SemesterSubjectJoins.Add(ss);
            await _context.SaveChangesAsync();

            var semester = await _context.Semesters.FindAsync(ss.SemesterId);
            var subject = await _context.Subjects.FindAsync(ss.SubjectId);

            return CreatedAtAction(nameof(GetSemesterSubjectJoin), new { id = ss.Id }, new SemesterSubjectJoinDto
            {
                Id = ss.Id,
                SemesterId = ss.SemesterId,
                SemesterName = semester.Name,
                SubjectId = ss.SubjectId,
                SubjectName = subject.Name
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSemesterSubjectJoin(int id, CreateSemesterSubjectJoinDto dto)
        {
            var ss = await _context.SemesterSubjectJoins.FindAsync(id);
            if (ss == null) return NotFound();

            ss.SemesterId = dto.SemesterId;
            ss.SubjectId = dto.SubjectId;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSemesterSubjectJoin(int id)
        {
            var ss = await _context.SemesterSubjectJoins.FindAsync(id);
            if (ss == null) return NotFound();

            _context.SemesterSubjectJoins.Remove(ss);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }

}

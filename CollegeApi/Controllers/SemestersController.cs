using CollegeApi.Data;
using CollegeApi.DTOs;
using CollegeApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CollegeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SemestersController : ControllerBase
    {
        private readonly AppDbContext _context;
        public SemestersController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SemesterDto>>> GetSemesters()
        {
            return await _context.Semesters
                .Select(s => new SemesterDto { Id = s.Id, Name = s.Name })
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SemesterDto>> GetSemester(int id)
        {
            var semester = await _context.Semesters.FindAsync(id);
            if (semester == null) return NotFound();
            return new SemesterDto { Id = semester.Id, Name = semester.Name };
        }

        [HttpPost]
        public async Task<ActionResult<SemesterDto>> CreateSemester(CreateSemesterDto dto)
        {
            var semester = new Semester { Name = dto.Name };
            _context.Semesters.Add(semester);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetSemester), new { id = semester.Id }, new SemesterDto { Id = semester.Id, Name = semester.Name });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSemester(int id, CreateSemesterDto dto)
        {
            var semester = await _context.Semesters.FindAsync(id);
            if (semester == null) return NotFound();
            semester.Name = dto.Name;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSemester(int id)
        {
            var semester = await _context.Semesters.FindAsync(id);
            if (semester == null) return NotFound();
            _context.Semesters.Remove(semester);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }

}

using CollegeApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CollegeApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Semester> Semesters { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<SemesterSubjectJoin> SemesterSubjectJoins { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SemesterSubjectJoin>()
                .HasOne(ss => ss.Semester)
                .WithMany(s => s.SemesterSubjects)
                .HasForeignKey(ss => ss.SemesterId);

            modelBuilder.Entity<SemesterSubjectJoin>()
                .HasOne(ss => ss.Subject)
                .WithMany(s => s.SemesterSubjects)
                .HasForeignKey(ss => ss.SubjectId);

            modelBuilder.Entity<Student>()
                .HasOne(s => s.Semester)
                .WithMany(s => s.Students)
                .HasForeignKey(s => s.SemesterId);
        }
    }
}

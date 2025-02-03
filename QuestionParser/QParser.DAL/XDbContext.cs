using Microsoft.EntityFrameworkCore;
using QParser.Models;

namespace QParser.DAL
{
    public class XDbContext : DbContext
    {
        public XDbContext(DbContextOptions<XDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Candidate>().ToTable("Candidate");
            modelBuilder.Entity<Course>().ToTable("Course");
            modelBuilder.Entity<History>().ToTable("History");
            modelBuilder.Entity<UserAnswer>().ToTable("UserAnswer");
        }

        public virtual DbSet<Candidate> Candidates { get; set; }
        public virtual DbSet<Course> Course { get; set; }
        public virtual DbSet<History> ExamResults { get; set; }
        public virtual DbSet<UserAnswer> UserAnswers { get; set; }
    }
}
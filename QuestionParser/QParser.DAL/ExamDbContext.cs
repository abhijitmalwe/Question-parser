using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using QParser.Models;

namespace QParser.DAL
{
    public class ExamDbContext : DbContext
    {
        public ExamDbContext(DbContextOptions<ExamDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Exam>().ToTable("Exam").HasMany(e => e.ExamParts);

            modelBuilder.Entity<ExamPart>().ToTable("ExamPart");
            modelBuilder.Entity<Question>().ToTable("Question").HasOne<ExamPart>(ep => ep.ExamPart);

            //modelBuilder.Entity<Question>().ToTable("Question").HasOne<ExamPart>(ep => ep.ExamPart)
            //    .WithMany(q => q.Questions).HasForeignKey(i => i.PartId);
            modelBuilder.Entity<Question>().HasMany(p => p.PossibleAnswers);

            modelBuilder.Entity<PossibleAnswer>().ToTable("PossibleAnswers");
            modelBuilder.Entity<Note>().ToTable("Note");
        }

        public virtual DbSet<Exam> Exams { get; set; }
        public virtual DbSet<ExamPart> ExamParts { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<PossibleAnswer> PossibleAnswers { get; set; }
        public virtual DbSet<Note> Notes { get; set; }

    }

    public class SqliteEncryptedConnection : SqliteConnection
    {
        private readonly string _password;

        public SqliteEncryptedConnection(string connectionString, string password) : base(connectionString)
        {
            _password = password;
        }

        public override void Open()
        {
            base.Open();
            using var command = CreateCommand();
            command.CommandText = "SELECT quote($password);";
            command.Parameters.AddWithValue("$password", _password);
            var quotedPassword = (string)command.ExecuteScalar();
            command.CommandText = $"PRAGMA key = {quotedPassword}";
            command.Parameters.Clear();
            command.ExecuteNonQuery();
        }
    }
}
using Microsoft.EntityFrameworkCore;
using QParser.Models;

namespace QParser.DAL
{
    public interface IRepositoryWrapper
    {
        IGenericDataRepository<Candidate> Candidate { get; }
        IGenericDataRepository<Course> Course { get; }
        IExamRepository Exam { get; }
        IGenericDataRepository<History> History { get; }
        IGenericDataRepository<Note> Note { get; }
        IPossibleAnswerRepository PossibleAnswer { get; }
        IQuestionRepository Question { get; }
        IGenericDataRepository<ExamPart> ExamPart { get; }
        IUserAnswerRepository UserAnswer { get; }
    }

    public class RepositoryWrapper : IRepositoryWrapper
    {
        protected ExamDbContext ExamDbcontext;
        protected XDbContext XDbcontext;

        private IGenericDataRepository<Candidate> _candidate;
        private IGenericDataRepository<Course> _course;
        private IGenericDataRepository<ExamPart> _examPart;
        private IExamRepository _exam;
        private IGenericDataRepository<History> _history;
        private IGenericDataRepository<Note> _note;
        private IPossibleAnswerRepository _possibleAnswer;
        private IQuestionRepository _question;
        private IUserAnswerRepository _userAnswer;

        public RepositoryWrapper(ExamDbContext context, XDbContext xDbcontext)
        {
            ExamDbcontext = context;
            XDbcontext = xDbcontext;
        }

        public RepositoryWrapper(string connectionString, string dbPassword)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ExamDbContext>();
            optionsBuilder.UseEncryptedSqlite(connectionString, dbPassword);
            ExamDbcontext = new ExamDbContext(optionsBuilder.Options);
        }

        public RepositoryWrapper(ExamDbContext context)
        {
            ExamDbcontext = context;
        }

        public IGenericDataRepository<Candidate> Candidate => _candidate ??= new GenericDataRepository<Candidate>(XDbcontext);

        public IGenericDataRepository<Course> Course => _course ??= new GenericDataRepository<Course>(XDbcontext);

        public IExamRepository Exam => _exam ??= new ExamRepository(ExamDbcontext);

        public IGenericDataRepository<History> History => _history ??= new GenericDataRepository<History>(XDbcontext);

        public IGenericDataRepository<Note> Note => _note ??= new GenericDataRepository<Note>(ExamDbcontext);

        public IPossibleAnswerRepository PossibleAnswer => _possibleAnswer ??= new PossibleAnswerRepository(ExamDbcontext);


        public IQuestionRepository Question => _question ??= new QuestionRepository(ExamDbcontext);
        public IGenericDataRepository<ExamPart> ExamPart => _examPart ??= new GenericDataRepository<ExamPart>(ExamDbcontext);

        public IUserAnswerRepository UserAnswer => _userAnswer ??= new UserAnswerRepository(XDbcontext);
    }
}
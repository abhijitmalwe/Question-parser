using System;
using System.Linq;
using System.Linq.Expressions;
using QParser.Models;

namespace QParser.DAL
{
    public interface IExamRepository : IGenericDataRepository<Exam>
    {
        Exam GetExam(Expression<Func<Exam, bool>> where);
    }

    public class ExamRepository : GenericDataRepository<Exam>, IExamRepository
    {
        public ExamRepository(ExamDbContext ctx) : base(ctx)
        {
        }

        public Exam GetExam(Expression<Func<Exam, bool>> where)
        {
            string[] partIds = {"1", "2", "3"};

            var exam = GetAll();

            return exam?.FirstOrDefault();
        }

    }
}
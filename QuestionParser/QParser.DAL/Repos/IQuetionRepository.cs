using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using QParser.Models;

namespace QParser.DAL
{
    public interface IQuestionRepository : IGenericDataRepository<Question>
    {
        Task SetAllQuestionsAsync();
        Task SetRandomQuestionsAsync(int[] questionsToSelect);
        Task SetRandomQuestionsAsync(int totalQuestions);
        Task SetQuestionsByRangeAsync(int minRange, int maxRange);

    }

    public class QuestionRepository : GenericDataRepository<Question>, IQuestionRepository
    {
        public QuestionRepository(ExamDbContext ctx) : base(ctx)
        {
        }

        public async Task<Question> GetSingle(Expression<Func<Question, bool>> where)
        {
            var question = await base.GetSingle(where, true);
            return question;
        }

        public async Task SetAllQuestionsAsync()
        {
            await RunSql("UPDATE QUESTION SET RandomSelect = 1;");
        }

        public async Task SetRandomQuestionsAsync(int[] questionsToSelect)
        {
            await RunSql("UPDATE QUESTION SET RandomSelect = 0 where RandomSelect <> 0;");
            await RunSql("UPDATE QUESTION SET RandomSelect = 1 where ID in (" +
                              string.Join(",", questionsToSelect) + ");");
        }

        public async Task SetRandomQuestionsAsync(int totalQuestions)
        {
            await RunSql("UPDATE QUESTION SET RandomSelect = 0 where RandomSelect <> 0;");
            await RunSql($"UPDATE QUESTION SET RandomSelect = 1 where ID in (SELECT id FROM Question ORDER BY RANDOM() LIMIT {totalQuestions});");
        }


        public async Task SetQuestionsByRangeAsync(int minRange, int maxRange)
        {
            await RunSql("UPDATE QUESTION SET RandomSelect = 0 where RandomSelect <> 0;");
            await RunSql($"UPDATE QUESTION SET RandomSelect = 1 where ID >= {minRange} and  ID <= {maxRange};");
        }

    }
}
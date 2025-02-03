using QParser.Models;

namespace QParser.DAL
{
    public interface IPossibleAnswerRepository : IGenericDataRepository<PossibleAnswer>
    {
    }

    public class PossibleAnswerRepository : GenericDataRepository<PossibleAnswer>, IPossibleAnswerRepository
    {
        public PossibleAnswerRepository(ExamDbContext ctx) : base(ctx)
        {
        }
    }
}
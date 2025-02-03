using System.Threading.Tasks;
using QParser.Models;

namespace QParser.DAL
{
    public interface IUserAnswerRepository : IGenericDataRepository<UserAnswer>
    {
        Task AddOrUpdate(UserAnswer userAnswer);
    }

    public class UserAnswerRepository : GenericDataRepository<UserAnswer>, IUserAnswerRepository
    {
        public UserAnswerRepository(XDbContext ctx) : base(ctx)
        {
        }

        public async Task AddOrUpdate(UserAnswer userAnswer)
        {
            if (userAnswer.Id > 0)
            {
                await Update(userAnswer);
            }
            else
            {
                await Add(userAnswer);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace QParser.DAL
{
    public interface IGenericDataRepository<T> where T : class
    {
        Task Add(params T[] items);

        Task AddWithChildAsync(params T[] items);

        void AddWithChild(params T[] items);

        IQueryable<T> GetAll();

        Task<IEnumerable<T>> GetAllAsync(bool eagerLoad, Expression<Func<T, bool>> where = null);


        Task<IList<T>> GetListAsync(Expression<Func<T, bool>> where, string oderByColumn = null, params Expression<Func<T, object>>[] navigationProperties);

        Task<PagedResult<T>> GetPagedListAsync(int pageIndex, int pageSize, Expression<Func<T, bool>> where,
            string oderByColumn = "Id", bool eagerLoad = false);

        Task<T> GetSingle(Expression<Func<T, bool>> where, bool eagerLoad = false);

        Task Remove(params T[] items);

        Task<int> RunSql(string sqlStatement);

        Task Update(params T[] items);
        IQueryable<T> GetQueryAble(bool eagerLoad);
    }
}
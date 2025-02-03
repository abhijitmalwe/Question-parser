using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using LazyCache;
using Microsoft.EntityFrameworkCore;
using QParser.Models;

namespace QParser.DAL
{
    public class GenericDataRepository<T> : IGenericDataRepository<T> where T : BaseEntity
    {
        protected DbContext Context;
        protected  IAppCache Cache ;
        private readonly TimeSpan _cacheSlidingExpiry;
        private readonly string _cachkey;
        public GenericDataRepository(DbContext ctx)
        {
            Context = ctx;
            _cacheSlidingExpiry = TimeSpan.FromHours(5);
            Cache= new CachingService();

            _cachkey = $"Get_Everything_{typeof(T).Name}";
        }

        private void RemoveCache()
        {
            Console.WriteLine("InitCache was called!");
            Cache?.Remove(_cachkey);
        }
        /// <summary>
        /// Get everything including child objects to last level. 
        /// </summary>
        /// <returns></returns>
        public virtual IQueryable<T> GetAll()
        {
            IQueryable<T> dbQuery = Context.Set<T>().Include(Context.GetIncludePaths(typeof(T)));

            Console.WriteLine(_cachkey);
            return Cache.GetOrAdd(_cachkey, () => dbQuery.AsNoTracking().ToList().AsQueryable(), _cacheSlidingExpiry);
        }

        /// <summary>
        /// GEt Everything based on the Where clause, including child objects to last level.
        /// </summary>
        /// <param name="eagerLoad"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public virtual async Task<IEnumerable<T>> GetAllAsync(bool eagerLoad, Expression<Func<T, bool>> where = null)
        {
            IQueryable<T> dbQuery = Context.Set<T>();
            if (eagerLoad)
                dbQuery = Context.Set<T>().Include(Context.GetIncludePaths(typeof(T)));
            
            if (where != null)
                dbQuery = dbQuery.Where(where);

            return await dbQuery.ToListAsync();
        }

        /// <summary>
        /// Gets objects based on the where clause and Navigation. It does not include related/children objects unless specified in the navigationProperties
        /// </summary>
        /// <param name="where"></param>
        /// <param name="oderByColumn"></param>
        /// <param name="navigationProperties"></param>
        /// <returns></returns>
        public virtual async Task<IList<T>> GetListAsync(Expression<Func<T, bool>> where, string oderByColumn = "Id", params Expression<Func<T, object>>[] navigationProperties)
        {
            IQueryable<T> dbQuery = Context.Set<T>();

            //Apply eager loading
            foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                dbQuery = dbQuery.Include(navigationProperty);

            if (where != null)
                dbQuery = dbQuery.Where(where);

            return await dbQuery.AsNoTracking().OrderBy(oderByColumn).ToListAsync();
        }


        public virtual async Task<PagedResult<T>> GetPagedListAsync(int pageIndex, int pageSize, Expression<Func<T, bool>> where, string oderByColumn = "Id", bool eagerLoad = false)
        {
            IQueryable<T> dbQuery = Context.Set<T>();
            if (eagerLoad)
                dbQuery = Context.Set<T>().Include(Context.GetIncludePaths(typeof(T)));
            
            if (where != null)
                dbQuery = dbQuery.Where(where);

            return await dbQuery.AsNoTracking().AsQueryable().OrderBy(oderByColumn).GetPagedAsync(pageIndex, pageSize);
        }

        public virtual async Task<T> GetSingle(Expression<Func<T, bool>> where, bool eagerLoad = false)
        {
            IQueryable<T> dbQuery = Context.Set<T>();
            if (eagerLoad)
                dbQuery = Context.Set<T>().Include(Context.GetIncludePaths(typeof(T)));

            return await dbQuery.AsNoTracking().FirstOrDefaultAsync(where);
        }

        public virtual IQueryable<T> GetQueryAble(bool eagerLoad)
        {
            IQueryable<T> dbQuery = Context.Set<T>();
            if (eagerLoad)
                dbQuery = Context.Set<T>().Include(Context.GetIncludePaths(typeof(T)));
               

            return  dbQuery.AsNoTracking();
        }

        public virtual async Task AddWithChildAsync(params T[] items)
        {
            foreach (T item in items)
            {
                if (item != null && item.Id <= 0)
                {
                    await Context.Set<T>().AddAsync(item);
                }
                else
                {
                    Context.Set<T>().Update(item ?? throw new InvalidOperationException());
                }
            }
            await Context.SaveChangesAsync();
            RemoveCache();

        }

        public virtual void AddWithChild(params T[] items)
        {
            foreach (T item in items)
            {
                if (item != null && item.Id <= 0)
                {
                    Context.Set<T>().Add(item);
                }
                else
                {
                    Context.Set<T>().Update(item ?? throw new InvalidOperationException());
                }
            }

            Context.SaveChanges();
            RemoveCache();

        }

        public virtual async Task Add(params T[] items)
        {

            foreach (T item in items)
            {
                if (item != null && item.Id <= 0)
                {
                    Context.Entry(item).State = EntityState.Added;
                }
                else
                {
                    Context.Entry(item ?? throw new InvalidOperationException()).State = EntityState.Modified;
                }
            }
            await Context.SaveChangesAsync();
            RemoveCache();

        }

        public virtual async Task Update(params T[] items)
        {
            foreach (T item in items)
            {
                if (item != null && Context.Entry(item).State != EntityState.Modified)
                {
                    Context.Entry(item).State = EntityState.Modified;
                }
            }
            await Context.SaveChangesAsync();
            RemoveCache();
        }

        public virtual async Task Remove(params T[] items)
        {
            foreach (T item in items)
            {
                if (item != null)
                 Context.Entry(item).State = EntityState.Deleted;
            }
            await Context.SaveChangesAsync();
            RemoveCache();
        }

        public virtual async Task<int> RunSql(string sqlStatement)
        {
            return await Context.Database.ExecuteSqlCommandAsync(sqlStatement);
        }

    }
}
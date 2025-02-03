using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace QParser.DAL
{
    public static class CustomExtensions
    {
        public static IQueryable<T> Include<T>(this IQueryable<T> source, IEnumerable<string> navigationPropertyPaths)
           where T : class
        {
            return navigationPropertyPaths.Aggregate(source, (query, path) => query.Include(path));
        }

        public static IEnumerable<string> GetIncludePaths(this DbContext context, Type clrEntityType)
        {
            var entityType = context.Model.FindEntityType(clrEntityType);
            var includedNavigation = new HashSet<INavigation>();
            var stack = new Stack<IEnumerator<INavigation>>();
            while (true)
            {
                var entityNavigation = new List<INavigation>();
                foreach (var navigation in entityType.GetNavigations())
                {
                    if (includedNavigation.Add(navigation))
                        entityNavigation.Add(navigation);
                }
                if (entityNavigation.Count == 0)
                {
                    if (stack.Count > 0)
                        yield return string.Join(".", stack.Reverse().Select(e => e?.Current?.Name));
                }
                else
                {
                    foreach (var navigation in entityNavigation)
                    {
                        var inverseNavigation = navigation.FindInverse();
                        if (inverseNavigation != null)
                            includedNavigation.Add(inverseNavigation);
                    }
                    stack.Push(entityNavigation.GetEnumerator());
                }
                while (stack.Count > 0 && !stack.Peek().MoveNext())
                    stack.Pop();
                if (stack.Count == 0) break;
                entityType = (stack.Peek().Current ?? throw new InvalidOperationException()).GetTargetType();
            }
        }


        public static async Task<PagedResult<T>> GetPagedAsync<T>(this IQueryable<T> query, int page, int pageSize) where T : class
        {
            var result = new PagedResult<T>
            {
                CurrentPage = page,
                PageSize = pageSize,
                RowCount = query.Count()
            };

            var pageCount = (double)result.RowCount / pageSize;
            result.PageCount = (int)Math.Ceiling(pageCount);

            var skip = (page - 1) * pageSize;
            result.Results = await query.Skip(skip).Take(pageSize).ToListAsync();

            return result;
        }

        public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> query, string memberName)
        {
            try
            {
                  if (string.IsNullOrEmpty(memberName))
                      memberName = "Id";

                  ParameterExpression[] typeParams = new ParameterExpression[] { Expression.Parameter(typeof(T), "") };

                  var orderby = "OrderBy";

                  if (!string.IsNullOrEmpty(memberName) && memberName.Contains(":"))
                  {
                      string[] orderBy = memberName.Split(':');
                      memberName = orderBy[0];
                      if (orderBy.Length > 1)
                      {
                          orderby = "OrderByDescending";
                      }
                  }

                  System.Reflection.PropertyInfo pi = typeof(T).GetProperty(memberName);
             
                  return (IOrderedQueryable<T>)query.Provider.CreateQuery(
                      Expression.Call(
                          typeof(Queryable),
                          orderby,
                          new[] { typeof(T), pi?.PropertyType },
                          query.Expression,
                          Expression.Lambda(Expression.Property(typeParams[0], pi), typeParams))
                  );
               
            }
            catch (Exception)
            {
                throw new Exception("The OrderBy clause has is wrong syntax. It must be:  columnName or Descending order try: columnName:desc");
            }
        }


    
        public static IQueryable<T> SortByDescending<T>(this IQueryable<T> queryable, Expression<Func<T, object>> sortFunc)
        {
            return queryable.OrderByDescending(sortFunc);
        }

        public static void UseEncryptedSqlite(this DbContextOptionsBuilder optionsBuilder, string connectionString, string password = null)
        {
            if (string.IsNullOrEmpty(password))
            {
                optionsBuilder.UseSqlite(connectionString);
            }
            else
            {
                using var connection = new SqliteEncryptedConnection(connectionString, password);
                connection.Open();
                optionsBuilder.UseSqlite(connection);
            }
        }
    }
}
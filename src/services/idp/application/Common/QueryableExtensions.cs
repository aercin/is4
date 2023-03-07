using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace application.Common
{
    public static class QueryableExtensions
    {
        public static IQueryable<TEntity> WhereIf<TEntity>(this IQueryable<TEntity> source, bool condition, Expression<Func<TEntity, bool>> predicate)
        {
            return condition ? source.Where(predicate) : source;
        }

        public static PaginatedList<TDto> QueryResource<TEntity, TDto>(this IQueryable<TEntity> resource,
                                                                            IMapper mapper, 
                                                                            int pageNumber = 1,
                                                                            int pageSize = 10)
                                                                                          where TEntity : class
                                                                                          where TDto : class
        {
            IQueryable<TEntity> query = resource.AsNoTracking(); 
            return query.ProjectTo<TDto>(mapper.ConfigurationProvider)
                        .MappedPaginatedList(pageNumber, pageSize);
        }
    }
}

namespace application.Common
{
    public static class MappingExtensions
    {
        public static PaginatedList<TDestination> MappedPaginatedList<TDestination>(this IQueryable<TDestination> queryable, int pageNumber, int pageSize)
        {
            return PaginatedList<TDestination>.Create(queryable, pageNumber, pageSize);
        }
    }
}



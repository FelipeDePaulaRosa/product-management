using System;
using System.Linq;

namespace CrossCutting.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> GetPaginated<T>(this IQueryable<T> query, int pageNumber, int pageSize)
        {
            if (pageNumber < 1)
                throw new ArgumentException("The page number must be greater than zero.");

            if (pageSize < 1)
                throw new ArgumentException("The page size must be greater than zero.");

            return query.Skip((pageNumber - 1) * pageSize).Take(pageSize);
        }
    }
}
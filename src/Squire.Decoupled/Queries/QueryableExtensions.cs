namespace Squire.Unhinged.Queries
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public static class QueryableExtensions
    {
        public static IOrderedQueryable<TModel> ApplyPaging<TModel, TResult>(this IOrderedQueryable<TModel> query, QueryWithConditions<TModel, TResult> conditions)
        {
            return (IOrderedQueryable<TModel>)query.Skip(conditions.PageSize * conditions.PageIndex).Take(conditions.PageSize);
        }

        public static IQueryable<TModel> ApplyPaging<TModel, TResult>(this IQueryable<TModel> query, QueryWithConditions<TModel, TResult> conditions)
        {
            return query.Skip(conditions.PageSize * conditions.PageIndex).Take(conditions.PageSize);
        }
    }
}

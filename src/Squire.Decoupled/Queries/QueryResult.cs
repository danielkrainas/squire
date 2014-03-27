namespace Squire.Decoupled.Queries
{
    using Squire.Validation;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class QueryResult<T> : IQueryResult<T>
        where T : class
    {
        public QueryResult(IEnumerable<T> items, int totalCount)
        {
            ValidationHelper.ArgumentNotNull(items, "items");
            this.Items = items;
            this.TotalCount = totalCount;
        }

        public IEnumerable<T> Items
        {
            get;
            private set;
        }

        public int TotalCount
        {
            get;
            private set;
        }
    }
}

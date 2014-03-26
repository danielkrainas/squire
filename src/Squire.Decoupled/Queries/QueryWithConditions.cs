namespace Squire.Unhinged.Queries
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class QueryWithConditions<TModel, TResult> : IQuery<TResult>
    {
        public QueryWithConditions()
        {
            this.PageIndex = 0;
            this.PageSize = 0;
        }

        public int PageIndex
        {
            get;
            set;
        }

        public int PageSize
        {
            get;
            set;
        }

        public virtual void Page(int pageIndex, int pageSize)
        {
            this.PageIndex = pageIndex;
            this.PageSize = pageSize;
        }
    }
}

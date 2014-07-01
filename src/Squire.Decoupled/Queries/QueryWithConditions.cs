namespace Squire.Decoupled.Queries
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

        public virtual QueryWithConditions<TModel, TResult> Page(int pageIndex, int pageSize)
        {
            this.PageIndex = pageIndex;
            this.PageSize = pageSize;
            return this;
        }

        public virtual void NextPage()
        {
            this.PageIndex++;
        }

        public virtual void PreviousPage()
        {
            this.PageIndex = Math.Max(this.PageIndex--, 0);
        }
    }
}

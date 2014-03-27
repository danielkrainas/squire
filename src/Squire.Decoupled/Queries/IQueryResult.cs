namespace Squire.Decoupled.Queries
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IQueryResult<out T>
        where T : class
    {
        IEnumerable<T> Items
        {
            get;
        }

        int TotalCount
        {
            get;
        }
    }
}

namespace Squire.Decoupled.Queries
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IDispatchQuery
    {
        TResult Execute<TResult>(IQuery<TResult> query);
    }
}

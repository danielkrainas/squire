namespace Squire.Unhinged.Queries
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IExecuteQuery<in TQuery, out TResult>
        where TQuery : IQuery<TResult>
    {
        TResult Execute(TQuery query);
    }
}

namespace Squire.Unhinged.Pipeline
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IDownstreamHandler
    {
        void HandleDownstream(IDownstreamContext context, IDownstreamMessage message);
    }
}

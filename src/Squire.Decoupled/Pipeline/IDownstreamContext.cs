namespace Squire.Unhinged.Pipeline
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IDownstreamContext
    {
        void SendUpstream(IUpstreamMessage message);

        void SendDownstream(IDownstreamMessage message);
    }
}

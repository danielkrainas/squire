namespace Squire.Unhinged.Pipeline
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class DevNullUpstreamHandler : IUpstreamHandler
    {
        public DevNullUpstreamHandler()
        {
        }

        public void HandleUpstream(IUpstreamContext context, IUpstreamMessage message)
        {
            
        }
    }
}

﻿namespace Squire.Decoupled.Pipeline
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IUpstreamHandler
    {
        void HandleUpstream(IUpstreamContext context, IUpstreamMessage message);
    }
}

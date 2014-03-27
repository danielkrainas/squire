namespace Squire.Decoupled.DomainEvents.Pipeline.Messages
{
    using Squire.Decoupled.Pipeline;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class TriggerWorker : IDownstreamMessage
    {
        public TriggerWorker()
        {
        }
    }
}

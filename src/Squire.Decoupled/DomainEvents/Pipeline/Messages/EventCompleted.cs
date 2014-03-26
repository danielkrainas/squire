namespace Squire.Unhinged.DomainEvents.Pipeline.Messages
{
    using Squire.Validation;
    using Squire.Unhinged.Pipeline;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class EventCompleted : IUpstreamMessage
    {
        public EventCompleted(DispatchEvent message)
        {
            ValidationHelper.ArgumentNotNull(message, "message");
            this.Message = message;
        }

        public DispatchEvent Message
        {
            get;
            private set;
        }
    }
}

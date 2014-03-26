namespace Squire.Unhinged.DomainEvents.Pipeline.Messages
{
    using Squire.Validation;
    using Squire.Unhinged.Pipeline;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class DispatchEvent : IDownstreamMessage
    {
        public DispatchEvent(IDomainEvent domainEvent)
        {
            ValidationHelper.ArgumentNotNull(domainEvent, "domainEvent");
            this.DomainEvent = domainEvent;
        }

        public IDomainEvent DomainEvent
        {
            get;
            private set;
        }
    }
}

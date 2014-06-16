namespace Squire.Decoupled.DomainEvents.Pipeline.Messages
{
    using Squire.Validation;
    using Squire.Decoupled.Pipeline;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class DispatchEvent : IDownstreamMessage
    {
        public DispatchEvent(IDomainEvent domainEvent)
        {
            domainEvent.VerifyParam("domainEvent").IsNotNull();
            this.DomainEvent = domainEvent;
        }

        public IDomainEvent DomainEvent
        {
            get;
            private set;
        }
    }
}

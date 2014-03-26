namespace Squire.Unhinged.DomainEvents
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class DomainEventBase : IDomainEvent
    {
        public DomainEventBase()
        {
            this.EventId = Guid.NewGuid();
        }

        public Guid EventId
        {
            get;
            private set;
        }
    }
}

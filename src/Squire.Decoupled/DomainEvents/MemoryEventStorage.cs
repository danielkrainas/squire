namespace Squire.Decoupled.DomainEvents
{
    using Squire.Validation;
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class MemoryEventStorage : IDomainEventStorage
    {
        private readonly ConcurrentDictionary<Guid, ICollection<IDomainEvent>> domainEvents;

        public MemoryEventStorage()
        {
            this.domainEvents = new ConcurrentDictionary<Guid, ICollection<IDomainEvent>>();
        }

        public void Hold(Guid batchId, IDomainEvent domainEvent)
        {
            domainEvent.VerifyParam("domainEvent").IsNotNull("you must specify a batchId");
            var events = this.domainEvents.GetOrAdd(batchId, e => new LinkedList<IDomainEvent>());
            events.Add(domainEvent);
        }

        public IEnumerable<IDomainEvent> Release(Guid batchId)
        {
            batchId.VerifyParam("batchId").IsNotEmpty("you must specify a batchId");
            ICollection<IDomainEvent> events;
            return this.domainEvents.TryGetValue(batchId, out events) ? events : new IDomainEvent[0];
        }

        public void Delete(Guid batchId)
        {
            batchId.VerifyParam("batchId").IsNotEmpty("you must specify a batchId");
            ICollection<IDomainEvent> events;
            if (!this.domainEvents.TryRemove(batchId, out events) && !this.domainEvents.TryRemove(batchId, out events))
            {
                throw new InvalidOperationException(string.Format("failed to remove events for batch '{0}'", batchId));
            }
        }
    }
}

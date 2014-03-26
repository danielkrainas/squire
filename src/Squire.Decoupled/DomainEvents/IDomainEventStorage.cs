namespace Squire.Unhinged.DomainEvents
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IDomainEventStorage
    {
        void Hold(Guid batchId, IDomainEvent domainEvent);

        IEnumerable<IDomainEvent> Release(Guid batchId);

        void Delete(Guid batchId);
    }
}

namespace Squire.Unhinged.DomainEvents
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IDomainEvent
    {
        Guid EventId
        {
            get;
        }
    }
}

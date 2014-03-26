namespace Squire.Unhinged.DomainEvents
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface ISubscribeOn<in TEvent>
        where TEvent : class, IDomainEvent
    {
        void Handle(TEvent domainEvent);
    }
}

namespace Squire.Unhinged.DomainEvents
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public interface IDomainEventDispatcher
    {
        void Dispatch<TEvent>(TEvent domainEvent) where TEvent : class, IDomainEvent;

        void Close();
    }
}

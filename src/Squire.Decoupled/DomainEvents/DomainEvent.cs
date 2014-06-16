namespace Squire.Decoupled.DomainEvents
{
    using Squire.Validation;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public static class DomainEvent
    {
        private static IDomainEventDispatcher _dispatcher;

        public static void Assign(IDomainEventDispatcher dispatcher)
        {
            dispatcher.VerifyParam("dispatcher").IsNotNull();
            DomainEvent._dispatcher = dispatcher;
        }

        public static void Publish<TEvent>(TEvent domainEvent)
            where TEvent : class, IDomainEvent
        {
            domainEvent.VerifyParam("domainEvent").IsNotNull();
            if (DomainEvent._dispatcher == null)
            {
                throw new InvalidOperationException("the default dispatcher has not been specified");
            }

            DomainEvent._dispatcher.Dispatch(domainEvent);
        }
    }
}

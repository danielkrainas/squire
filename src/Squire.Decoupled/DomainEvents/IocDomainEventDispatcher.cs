namespace Squire.Decoupled.DomainEvents
{
    using Microsoft.Practices.ServiceLocation;
    using Squire.Validation;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class IocDomainEventDispatcher : IDomainEventDispatcher
    {
        private readonly IServiceLocator locator;

        public IocDomainEventDispatcher(IServiceLocator locator)
        {
            locator.VerifyParam("locator").IsNotNull();
            this.locator = locator;
        }

        public void Dispatch<TEvent>(TEvent domainEvent) 
            where TEvent : class, IDomainEvent
        {
            domainEvent.VerifyParam("domainEvent").IsNotNull();
            foreach (var handler in locator.GetAllInstances<ISubscribeOn<TEvent>>())
            {
                handler.Handle(domainEvent);
            }
        }

        public void Close()
        {
        }
    }
}

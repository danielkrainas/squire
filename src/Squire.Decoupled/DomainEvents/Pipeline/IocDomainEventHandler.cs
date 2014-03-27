namespace Squire.Decoupled.DomainEvents.Pipeline
{
    using Squire.Validation;
    using Squire.Decoupled.DomainEvents.Pipeline.Messages;
    using Squire.Decoupled.Pipeline;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.Practices.ServiceLocation;

    public class IocDomainEventHandler : IDownstreamHandler
    {
        private readonly MethodInfo method;

        private readonly IServiceLocator locator;

        public IocDomainEventHandler(IServiceLocator locator)
        {
            locator.VerifyParam("locator").IsNotNull();
            this.locator = locator;
            this.method = this.GetType().GetMethod("Dispatch", BindingFlags.Instance | BindingFlags.NonPublic);
        }

        public void HandleDownstream(IDownstreamContext context, IDownstreamMessage message)
        {
            var dispatch = message as DispatchEvent;
            if (dispatch != null)
            {
                try
                {
                    this.method.MakeGenericMethod(dispatch.DomainEvent.GetType()).Invoke(this, new object[] { dispatch.DomainEvent });
                }
                catch (Exception e)
                {
                    context.SendUpstream(new EventFailed(dispatch, e));
                }
            }
        }

        private void Dispatch<TEvent>(TEvent domainEvent)
            where TEvent : class, IDomainEvent
        {
            foreach (var subscriber in this.locator.GetAllInstances<ISubscribeOn<TEvent>>())
            {
                subscriber.Handle(domainEvent);
            }
        }
    }
}

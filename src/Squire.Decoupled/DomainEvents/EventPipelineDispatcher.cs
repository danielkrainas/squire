namespace Squire.Decoupled.DomainEvents
{
    using Squire.Validation;
    using Squire.Decoupled.DomainEvents.Pipeline.Messages;
    using Squire.Decoupled.Pipeline;
    using Squire.Decoupled.Pipeline.Messages;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class EventPipelineDispatcher : IDomainEventDispatcher
    {
        private readonly IPipeline pipeline;

        public EventPipelineDispatcher(IPipeline pipeline)
        {
            ValidationHelper.ArgumentNotNull(pipeline, "pipeline");
            this.pipeline = pipeline;
        }

        public void Dispatch<TEvent>(TEvent domainEvent)
            where TEvent : class, IDomainEvent
        {
            this.pipeline.Send(new DispatchEvent(domainEvent));
        }

        public void Close()
        {
            this.pipeline.Send(new Shutdown());
        }
    }
}

namespace Squire.Decoupled.DomainEvents
{
    using Squire.Validation;
    using Squire.Decoupled.DomainEvents.Pipeline;
    using Squire.Decoupled.Pipeline;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.Practices.ServiceLocation;

    public class EventPipelineBuilder
    {
        private readonly IUpstreamHandler errorHandler;

        private IDownstreamHandler inner;

        private int maxWorkers;

        private IDomainEventStorage storage;

        private IUnitOfWorkAdapter unitOfWorkAdapter;

        public EventPipelineBuilder(IUpstreamHandler errorHandler)
        {
            errorHandler.VerifyParam("errorHandler").IsNotNull();
            this.errorHandler = errorHandler;
            this.storage = new MemoryEventStorage();
            this.maxWorkers = 0;
        }

        public EventPipelineBuilder Asynchronous(int maxWorkers = 5)
        {
            this.maxWorkers = maxWorkers;
            return this;
        }

        public EventPipelineBuilder StoreEvents(IDomainEventStorage storage)
        {
            storage.VerifyParam("storage").IsNotNull();
            this.storage = storage;
            return this;
        }

        public EventPipelineBuilder UseLocator(IServiceLocator locator)
        {
            locator.VerifyParam("locator").IsNotNull();
            this.inner = new IocDomainEventHandler(locator);
            return this;
        }

        public EventPipelineBuilder UseCustomDispatcher(IDownstreamHandler dispatcher)
        {
            dispatcher.VerifyParam("dispatcher").IsNotNull();
            this.inner = dispatcher;
            return this;
        }

        public EventPipelineBuilder WaitOnTransactions(IUnitOfWorkAdapter adapter)
        {
            adapter.VerifyParam("adapter").IsNotNull();
            this.unitOfWorkAdapter = adapter;
            return this;
        }

        public IDomainEventDispatcher Build()
        {
            if (this.inner == null)
            {
                throw new InvalidOperationException("You must specify either a container or a customer handler.");
            }

            var builder = new PipelineBuilder();
            if (this.unitOfWorkAdapter != null)
            {
                builder.RegisterDownstream(new TransactionalDomainEventHandler(this.unitOfWorkAdapter, this.storage));
            }

            if (this.maxWorkers > 0)
            {
                builder.RegisterDownstream(new AsyncDomainEventHandler(this.maxWorkers));
            }

            builder.RegisterDownstream(this.inner);
            builder.RegisterUpstream(this.errorHandler);
            var pipeline = builder.Build();
            pipeline.Start();
            return new EventPipelineDispatcher(pipeline);
        }
    }
}

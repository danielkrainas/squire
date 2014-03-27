namespace Squire.Decoupled.Commands
{
    using Squire.Validation;
    using Squire.Decoupled.Commands.Pipeline;
    using Squire.Decoupled.DomainEvents.Pipeline;
    using Squire.Decoupled.Pipeline;
    using Microsoft.Practices.ServiceLocation;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class PipelineDispatcherBuilder
    {
        private readonly IUpstreamHandler errorHandler;

        private IServiceLocator container;

        private IDownstreamHandler lastHandler;

        private int maxAttempts;

        private StorageCommandHandler storageHandler;

        private int workers;

        public PipelineDispatcherBuilder(IUpstreamHandler errorHandler)
        {
            errorHandler.VerifyParam("errorHandler").IsNotNull();
            this.errorHandler = errorHandler;
            this.workers = 0;
            this.maxAttempts = 0;
            this.storageHandler = null;
            this.lastHandler = null;
            this.container = null;
        }

        public PipelineDispatcherBuilder StoreCommands(ICommandStorage storage)
        {
            storage.VerifyParam("storage").IsNotNull();
            this.storageHandler = new StorageCommandHandler(storage);
            return this;
        }

        public PipelineDispatcherBuilder AsyncDispatching(int maxConcurrentCommands)
        {
            maxConcurrentCommands.VerifyParam("maxConcurrentCommands").IsGreaterThanZero();
            this.workers = maxConcurrentCommands;
            return this;
        }

        public PipelineDispatcherBuilder RetryCommands(int maxAttempts)
        {
            maxAttempts.VerifyParam("maxAttempts").IsGreaterThanZero();
            this.maxAttempts = maxAttempts;
            return this;
        }

        public PipelineDispatcherBuilder UseContainer(IServiceLocator container)
        {
            container.VerifyParam("container").IsNotNull();
            this.container = container;
            return this;
        }

        public PipelineDispatcherBuilder Dispatcher(IDownstreamHandler lastHandler)
        {
            lastHandler.VerifyParam("lastHandler").IsNotNull();
            this.lastHandler = lastHandler;
            return this;
        }

        public ICommandDispatcher Build()
        {
            if (this.lastHandler == null && this.container == null)
            {
                throw new InvalidOperationException("you must specify a single handler that can actually invoke the correct command handler.");
            }

            if (this.lastHandler != null && this.container != null)
            {
                throw new InvalidOperationException("you may only specify either a dispatcher or a container.");
            }

            var builder = new PipelineBuilder();
            if (this.maxAttempts > 0)
            {
                builder.RegisterUpstream(new RetryingCommandHandler(this.maxAttempts));
            }

            if (this.storageHandler != null)
            {
                builder.RegisterDownstream(this.storageHandler);
                builder.RegisterUpstream(this.storageHandler);
            }

            if (this.workers > 0)
            {
                builder.RegisterDownstream(new AsyncCommandHandler(this.workers));
            }

            if (this.container != null)
            {
                builder.RegisterDownstream(new IocCommandHandler(this.container));
            }
            else
            {
                builder.RegisterDownstream(this.lastHandler);
            }

            builder.RegisterUpstream(this.errorHandler);
            var pipeline = builder.Build();
            pipeline.Start();
            return new PipelineDispatcher(pipeline);
        }
    }
}

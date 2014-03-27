namespace Squire.Decoupled.Pipeline
{
    using Squire.Validation;
    using Squire.Decoupled.Commands.Pipeline.Messages;
    using Squire.Decoupled.DomainEvents.Pipeline.Messages;
    using Squire.Decoupled.Pipeline.Messages;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Pipeline : IPipeline, IUpstreamHandler, IDownstreamHandler
    {
        private readonly List<DownstreamContext> downstream;

        private readonly List<UpstreamContext> upstream;

        private readonly DownstreamContext downContext;

        private readonly UpstreamContext upContext;

        private bool locked;

        public Pipeline()
        {
            this.downContext = new DownstreamContext(this);
            this.upContext = new UpstreamContext(this);
            this.downstream = new List<DownstreamContext>();
            this.upstream = new List<UpstreamContext>();
            this.locked = false;
        }

        void IDownstreamHandler.HandleDownstream(IDownstreamContext context, IDownstreamMessage message)
        {
            if (message is DispatchEvent)
            {
                this.upstream[0].Invoke(new EventCompleted((DispatchEvent)message));
            }
            else if (message is DispatchCommand)
            {
                this.upstream[0].Invoke(new CommandCompleted((DispatchCommand)message));
            }
        }

        void IUpstreamHandler.HandleUpstream(IUpstreamContext context, IUpstreamMessage message)
        {
            throw new InvalidOperationException(this.upstream[this.upstream.Count - 2] + " invoked SendUpstream when its the last handler. Don't do that..");
        }

        public void Send(IDownstreamMessage message)
        {
            message.VerifyParam("message").IsNotNull();
            if (!this.locked)
            {
                throw new InvalidOperationException("Start must be called before using the pipeline.");
            }

            this.downstream[0].Invoke(message);
        }

        public void Start()
        {
            this.locked = true;
            this.Send(new StartHandlers());
        }

        public void Add(UpstreamContext context)
        {
            context.VerifyParam("context").IsNotNull();
            if (this.locked)
            {
                throw new InvalidOperationException("The pipeline may not be modified after Start() has been called.");
            }


            this.upstream.Add(context);
        }

        public void Add(DownstreamContext context)
        {
            context.VerifyParam("context").IsNotNull();
            if (this.locked)
            {
                throw new InvalidOperationException("The pipeline may not be modified after Start() has been called.");
            }

            if (context.IsLast)
            {
                context.SetNext(this.downContext);
            }

            this.downstream.Add(context);
        }
    }
}

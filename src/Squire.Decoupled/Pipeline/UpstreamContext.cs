namespace Squire.Unhinged.Pipeline
{
    using Squire.Validation;
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class UpstreamContext : IUpstreamContext
    {
        private readonly ConcurrentQueue<IDownstreamMessage> downMessages;

        private readonly ConcurrentQueue<IUpstreamMessage> upMessages;

        private readonly IUpstreamHandler handler;

        private DownstreamContext down;

        private UpstreamContext next;

        public UpstreamContext(IUpstreamHandler handler)
        {
            this.downMessages = new ConcurrentQueue<IDownstreamMessage>();
            this.upMessages = new ConcurrentQueue<IUpstreamMessage>();
            this.handler = handler;
        }

        public void SendUpstream(IUpstreamMessage message)
        {
            this.upMessages.Enqueue(message);
        }

        public void SendDownstream(IDownstreamMessage message)
        {
            this.downMessages.Enqueue(message);
        }

        public void SetNext(UpstreamContext next)
        {
            next.VerifyParam("next").IsNotNull();
            this.next = next;
        }

        public void SetDownstream(DownstreamContext previous)
        {
            previous.VerifyParam("previous").IsNotNull();
            this.down = previous;
        }

        public void Invoke(IUpstreamMessage message)
        {
            this.handler.HandleUpstream(this, message);
            this.InvokeUpstream();
            this.InvokeDownstream();
        }

        public void InvokeUpstream()
        {
            if (this.next == null && this.upMessages.Count > 0)
            {
                throw new InvalidOperationException(string.Format("There are no more upstream handlers after '{0}'.", this.handler.GetType().FullName));
            }

            if (this.next != null)
            {
                IUpstreamMessage message;
                while (this.upMessages.TryDequeue(out message))
                {
                    this.next.Invoke(message);
                }
            }
        }

        public void InvokeDownstream()
        {
            IDownstreamMessage message;
            while (this.downMessages.TryDequeue(out message))
            {
                this.down.Invoke(message);
            }
        }
    }
}

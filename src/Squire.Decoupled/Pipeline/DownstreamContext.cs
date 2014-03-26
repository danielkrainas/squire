namespace Squire.Unhinged.Pipeline
{
    using Squire.Validation;
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class DownstreamContext : IDownstreamContext
    {
        private readonly ConcurrentQueue<IUpstreamMessage> upMessages;

        private readonly ConcurrentQueue<IDownstreamMessage> downMessages;

        private readonly IDownstreamHandler handler;

        private UpstreamContext up;

        private DownstreamContext next;

        public DownstreamContext(IDownstreamHandler handler)
        {
            this.handler = handler;
            this.upMessages = new ConcurrentQueue<IUpstreamMessage>();
            this.downMessages = new ConcurrentQueue<IDownstreamMessage>();
        }

        public bool IsLast
        {
            get
            {
                return this.next == null;
            }
        }

        public void Invoke(IDownstreamMessage message)
        {
            ValidationHelper.ArgumentNotNull(message, "message");
            this.handler.HandleDownstream(this, message);
            this.InvokeUpstream();
            this.InvokeDownstream();
        }

        public void SendUpstream(IUpstreamMessage message)
        {
            ValidationHelper.ArgumentNotNull(message, "message");
            this.upMessages.Enqueue(message);
        }

        public void SendDownstream(IDownstreamMessage message)
        {
            ValidationHelper.ArgumentNotNull(message, "message");
            this.downMessages.Enqueue(message);
        }

        public void SetNext(DownstreamContext next)
        {
            ValidationHelper.ArgumentNotNull(next, "next");
            this.next = next;
        }

        public void SetUpstream(UpstreamContext up)
        {
            ValidationHelper.ArgumentNotNull(up, "up");
            this.up = up;
        }

        public void InvokeUpstream()
        {
            if (this.next == null && this.upMessages.Count > 0)
            {
                throw new InvalidOperationException("no upstream handler has been set.");
            }

            IUpstreamMessage message;
            while (this.upMessages.TryDequeue(out message))
            {
                this.up.Invoke(message);
            }
        }

        public void InvokeDownstream()
        {
            if (this.next == null && this.downMessages.Count > 0)
            {
                throw new InvalidOperationException(string.Format("There is no downstream handlers after '{0}'", this.handler.GetType().FullName));
            }

            IDownstreamMessage message;
            while (this.downMessages.TryDequeue(out message))
            {
                this.next.Invoke(message);
            }
        }
    }
}

namespace Squire.Decoupled.DomainEvents.Pipeline
{
    using Squire.Decoupled.DomainEvents.Pipeline.Messages;
    using Squire.Decoupled.Pipeline;
    using Squire.Decoupled.Pipeline.Messages;
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    public class AsyncDomainEventHandler : IDownstreamHandler
    {
        private readonly int maxWorkers = 5;

        private readonly ConcurrentQueue<DispatchEvent> queue;

        private readonly ManualResetEventSlim shutdownEvent;

        private IDownstreamContext context;

        private long currentWorkers;

        private bool shutdown;

        public AsyncDomainEventHandler(int maxWorkers)
        {
            this.queue = new ConcurrentQueue<DispatchEvent>();
            this.shutdownEvent = new ManualResetEventSlim();
            this.maxWorkers = maxWorkers;
            this.shutdown = false;
            this.currentWorkers = 0;
        }

        public void HandleDownstream(IDownstreamContext context, IDownstreamMessage message)
        {
            this.context = context;
            var dispatchMessage = message as DispatchEvent;
            if (dispatchMessage != null)
            {
                this.queue.Enqueue(dispatchMessage);
                if (Interlocked.Read(ref this.currentWorkers) < this.maxWorkers)
                {
                    Interlocked.Increment(ref this.currentWorkers);
                    ThreadPool.QueueUserWorkItem(this.DispatchEventsNow);
                }
            }
            else if (message is Shutdown)
            {
                this.Close();
            }
        }

        public void Close()
        {
            if (Interlocked.Read(ref this.currentWorkers) > 0)
            {
                this.shutdown = true;
                if (!this.shutdownEvent.Wait(TimeSpan.FromSeconds(5)))
                {
                    throw new InvalidOperationException("failed to shutdown all events before shutdown");
                }
            }
        }

        private void DispatchEventsNow(object state)
        {
            try
            {
                while (this.DispatchEventNow()) ;
            }
            finally
            {
                Interlocked.Decrement(ref this.currentWorkers);
                if (this.currentWorkers == 0 && this.shutdown)
                {
                    this.shutdownEvent.Set();
                }
            }
        }

        private bool DispatchEventNow()
        {
            DispatchEvent targetEvent;
            if (this.queue.TryDequeue(out targetEvent))
            {
                this.context.SendDownstream(targetEvent);
                return true;
            }

            return false;
        }
    }
}

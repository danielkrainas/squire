namespace Squire.Decoupled.Commands.Pipeline
{
    using Squire.Decoupled.Commands.Pipeline.Messages;
    using Squire.Decoupled.Pipeline;
    using Squire.Decoupled.Pipeline.Messages;
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    public class AsyncCommandHandler : IDownstreamHandler
    {
        private readonly ManualResetEventSlim closingEvent;

        private readonly ConcurrentQueue<DispatchCommand> commands;

        private readonly int maxConcurrentTasks;

        private bool shutdown;

        private IDownstreamContext context;

        private long currentWorkers;

        public AsyncCommandHandler(int maxConcurrentTasks)
        {
            this.closingEvent = new ManualResetEventSlim(false);
            this.shutdown = false;
            this.commands = new ConcurrentQueue<DispatchCommand>();
            this.maxConcurrentTasks = maxConcurrentTasks;
        }

        public void HandleDownstream(IDownstreamContext context, IDownstreamMessage message)
        {
            this.context = context;
            if (message is DispatchCommand)
            {
                this.EnqueueCommand((DispatchCommand)message);
            }
            else if (message is Shutdown)
            {
                this.Close();
            }
            else if (message is StartHandlers)
            {
                this.StartWorker();
            }

            context.SendDownstream(message);
        }

        public void Close()
        {
            this.shutdown = true;
            if (Interlocked.Read(ref this.currentWorkers) > 0)
            {
                this.closingEvent.Wait(TimeSpan.FromSeconds(10));
            }
        }

        private void DispatchCommands()
        {
            try
            {
                while (this.DispatchCommand()) ;
            }
            finally
            {
                Interlocked.Decrement(ref this.currentWorkers);
                if (this.currentWorkers == 0 && this.shutdown)
                {
                    this.closingEvent.Set();
                }
            }
        }

        private bool DispatchCommand()
        {
            DispatchCommand command;
            if (!this.commands.TryDequeue(out command))
            {
                return false;
            }

            try
            {
                this.context.SendDownstream(command);
            }
            catch (Exception e)
            {
                this.context.SendUpstream(new PipelineFailure(this, command, "AsyncHandler failed to dispatch commands.", e));
            }

            return true;
        }

        private void EnqueueCommand(DispatchCommand command)
        {
            this.commands.Enqueue(command);
            if (this.shutdown)
            {
                return;
            }

            this.StartWorker();
        }

        private void StartWorker()
        {
            if (Interlocked.Read(ref this.currentWorkers) < this.maxConcurrentTasks)
            {
                Interlocked.Increment(ref this.currentWorkers);
                ThreadPool.QueueUserWorkItem(x => DispatchCommands());
            }
            else
            {
                throw new InvalidOperationException("max workers reached.");
            }
        }
    }
}

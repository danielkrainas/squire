namespace Squire.Decoupled.Commands
{
    using Squire.Validation;
    using Squire.Decoupled.Commands.Pipeline.Messages;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class MemoryCommandStorage : ICommandStorage
    {
        private class StorageEntry
        {
            public StorageEntry(DispatchCommand command)
            {
                this.Command = command;
            }

            public Guid CommandId
            {
                get
                {
                    return this.Command.Command.CommandId;
                }
            }

            public DispatchCommand Command
            {
                get;
                set;
            }

            public DateTime StartedAt
            {
                get;
                set;
            }
        }

        private readonly object mutex;

        private readonly List<StorageEntry> queue;

        public MemoryCommandStorage()
        {
            this.mutex = new object();
            this.queue = new List<StorageEntry>();
        }

        public void Add(DispatchCommand command)
        {
            ValidationHelper.ArgumentNotNull(command, "command");
            lock (this.mutex)
            {
                this.queue.Add(new StorageEntry(command));
            }
        }

        public void Delete(ICommand command)
        {
            lock (this.queue)
            {
                this.queue.RemoveAll(i => i.CommandId == command.CommandId);
            }
        }

        public void Update(DispatchCommand command)
        {
            lock (this.mutex)
            {
                var entry = this.queue.FirstOrDefault(i => i.CommandId == command.Command.CommandId);
                if (entry != null)
                {
                    entry.StartedAt = DateTime.MinValue;
                }
            }
        }

        public DispatchCommand Dequeue()
        {
            StorageEntry entry = null;
            lock (this.queue)
            {
                entry = this.queue.FirstOrDefault(i => i.StartedAt == DateTime.MinValue);
                if (entry == null)
                {
                    return null;
                }

                entry.StartedAt = DateTime.Now;
            }

            return entry.Command;
        }

        public IEnumerable<DispatchCommand> FindFailedCommands(DateTime processedBefore)
        {
            lock (this.queue)
            {
                return this.queue.Where(i => i.StartedAt < processedBefore).Select(i => i.Command).ToList();
            }
        }
    }
}

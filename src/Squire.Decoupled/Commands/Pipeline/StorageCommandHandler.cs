namespace Squire.Decoupled.Commands.Pipeline
{
    using Squire.Validation;
    using Squire.Decoupled.Commands.Pipeline.Messages;
    using Squire.Decoupled.Pipeline;
    using Squire.Decoupled.Pipeline.Messages;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class StorageCommandHandler : IUpstreamHandler, IDownstreamHandler
    {
        private readonly ICommandStorage storage;

        public StorageCommandHandler(ICommandStorage storage)
        {
            ValidationHelper.ArgumentNotNull(storage, "storage");
            this.storage = storage;
        }

        public void HandleDownstream(IDownstreamContext context, IDownstreamMessage message)
        {
            if (message is DispatchCommand)
            {
                this.storage.Add(message as DispatchCommand);
            }
            else if (message is StartHandlers)
            {
                context.SendDownstream(message);
                var commands = this.storage.FindFailedCommands(DateTime.Now.AddSeconds(-5));
                foreach (var command in commands)
                {
                    context.SendDownstream(command);
                }
            }
            else
            {
                context.SendDownstream(message);
            }
        }

        public void HandleUpstream(IUpstreamContext context, IUpstreamMessage message)
        {
            if (message is CommandCompleted)
            {
                storage.Delete(((CommandCompleted)message).Message.Command);
            }
            else if (message is CommandAborted)
            {
                storage.Delete(((CommandAborted)message).Message.Command);
            }
            else if (message is CommandFailed)
            {
                storage.Update(((CommandFailed)message).Message);
            }

            context.SendUpstream(message);
        }
    }
}

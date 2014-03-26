namespace Squire.Unhinged.Commands.Pipeline
{
    using Squire.Unhinged.Commands.Pipeline.Messages;
    using Squire.Unhinged.Pipeline;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class RetryingCommandHandler : IUpstreamHandler
    {
        private readonly int attempts;

        public RetryingCommandHandler(int attempts)
        {
            this.attempts = attempts;
        }

        public void HandleUpstream(IUpstreamContext context, IUpstreamMessage message)
        {
            var failed = message as CommandFailed;
            if (failed != null)
            {
                if (failed.Message.Attempts >= this.attempts)
                {
                    context.SendUpstream(new CommandAborted(failed.Message, failed.Exception));
                }
                else
                {
                    context.SendDownstream(failed.Message);
                }
            }

            context.SendUpstream(message);
        }

        public void Close()
        {

        }
    }
}

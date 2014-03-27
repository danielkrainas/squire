namespace Squire.Decoupled.Commands.Pipeline.Messages
{
    using Squire.Validation;
    using Squire.Decoupled.Pipeline;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class CommandCompleted : IUpstreamMessage
    {
        public CommandCompleted(DispatchCommand message)
        {
            ValidationHelper.ArgumentNotNull(message, "message");
            this.Message = message;
        }

        public DispatchCommand Message
        {
            get;
            private set;
        }
    }
}

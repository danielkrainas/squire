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
            message.VerifyParam("message").IsNotNull();
            this.Message = message;
        }

        public DispatchCommand Message
        {
            get;
            private set;
        }
    }
}

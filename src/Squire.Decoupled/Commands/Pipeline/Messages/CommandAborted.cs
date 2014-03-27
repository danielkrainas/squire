namespace Squire.Decoupled.Commands.Pipeline.Messages
{
    using Squire.Validation;
    using Squire.Decoupled.Pipeline;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class CommandAborted : IUpstreamMessage
    {
        public CommandAborted(DispatchCommand message, Exception exception)
        {
            ValidationHelper.ArgumentNotNull(message, "message");
            ValidationHelper.ArgumentNotNull(exception, "exception");
            this.Message = message;
            this.Exception = exception;
        }

        public DispatchCommand Message
        {
            get;
            private set;
        }

        public Exception Exception
        {
            get;
            private set;
        }
    }
}

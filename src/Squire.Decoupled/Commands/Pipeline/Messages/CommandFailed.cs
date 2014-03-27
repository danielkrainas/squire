namespace Squire.Decoupled.Commands.Pipeline.Messages
{
    using Squire.Validation;
    using Squire.Decoupled.Pipeline;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class CommandFailed : IUpstreamMessage
    {
        private readonly DispatchCommand message;

        private readonly Exception exception;

        public CommandFailed(DispatchCommand message, Exception exception)
        {
            ValidationHelper.ArgumentNotNull(message, "message");
            ValidationHelper.ArgumentNotNull(exception, "exception");
            this.message = message;
            this.exception = exception;
        }

        public int Attempts
        {
            get
            {
                return this.message.Attempts;
            }
        }

        public Exception Exception
        {
            get
            {
                return this.exception;
            }
        }

        public DispatchCommand Message
        {
            get
            {
                return this.message;
            }
        }

        public override string ToString()
        {
            return string.Format("Command '{0}' failed, Attempts: {1} \r\n{2}", this.Message, this.Attempts, this.Exception);
        }
    }
}

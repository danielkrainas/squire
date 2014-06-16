namespace Squire.Decoupled.DomainEvents.Pipeline.Messages
{
    using Squire.Validation;
    using Squire.Decoupled.Pipeline;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class EventFailed : IUpstreamMessage
    {
        private readonly Exception exception;

        private readonly DispatchEvent message;

        public EventFailed(DispatchEvent message, Exception exception)
        {
            message.VerifyParam("message").IsNotNull();
            exception.VerifyParam("exception").IsNotNull();
            this.message = message;
            this.exception = exception;
        }

        public DispatchEvent Message
        {
            get
            {
                return this.message;
            }
        }

        public Exception Exception
        {
            get
            {
                return this.exception;
            }
        }
    }
}

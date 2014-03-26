namespace Squire.Unhinged.DomainEvents.Pipeline.Messages
{
    using Squire.Validation;
    using Squire.Unhinged.Pipeline;
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
            ValidationHelper.ArgumentNotNull(message, "message");
            ValidationHelper.ArgumentNotNull(exception, "exception");
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

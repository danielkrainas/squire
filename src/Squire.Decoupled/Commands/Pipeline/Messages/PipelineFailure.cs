namespace Squire.Unhinged.Commands.Pipeline.Messages
{
    using Squire.Validation;
    using Squire.Unhinged.Pipeline;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class PipelineFailure : IUpstreamMessage
    {
        public PipelineFailure(object handler, object message, string errorMessage, Exception exception)
        {
            handler.VerifyParam("handler").IsNotNull();
            message.VerifyParam("message").IsNotNull();
            errorMessage.VerifyParam("errorMessage").IsNotNull();
            this.Handler = handler;
            this.Message = message;
            this.ErrorMessage = errorMessage;
            this.Exception = exception;
        }

        public string ErrorMessage
        {
            get;
            private set;
        }

        public object Handler
        {
            get;
            private set;
        }

        public object Message
        {
            get;
            set;
        }

        public Exception Exception
        {
            get;
            private set;
        }

        public override string ToString()
        {
            return string.Format("'{0}' failed to handle a message '{1}' due to: {2}\r\n{3}", this.Handler.GetType().FullName, this.Message, this.ErrorMessage, this.Exception);
        }
    }
}

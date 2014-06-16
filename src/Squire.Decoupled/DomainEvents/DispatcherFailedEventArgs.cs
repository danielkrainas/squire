namespace Squire.Decoupled.DomainEvents
{
    using Squire.Validation;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class DispatcherFailedEventArgs : EventArgs
    {
        public DispatcherFailedEventArgs(Exception exception, IDomainEvent domainEvent)
        {
            exception.VerifyParam("exception").IsNotNull();
            domainEvent.VerifyParam("domainEvent").IsNotNull();
            this.Exception = exception;
            this.DomainEvent = domainEvent;
        }

        public IDomainEvent DomainEvent
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

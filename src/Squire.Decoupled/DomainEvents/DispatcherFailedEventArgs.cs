namespace Squire.Unhinged.DomainEvents
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
            ValidationHelper.ArgumentNotNull(exception, "exception");
            ValidationHelper.ArgumentNotNull(domainEvent, "domainEvent");
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

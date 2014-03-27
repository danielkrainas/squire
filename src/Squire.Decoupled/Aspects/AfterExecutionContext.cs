namespace Squire.Decoupled.Aspects
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;

    public class AfterExecutionContext
    {
        public AfterExecutionContext(JoinPointDescriptor joinPoint)
            : this()
        {
            this.JoinPoint = joinPoint;
        }

        public AfterExecutionContext(JoinPointDescriptor joinPoint, bool canceled, Exception exception)
            : this(joinPoint)
        {
            this.Canceled = canceled;
            this.Exception = exception;
        }

        public AfterExecutionContext()
        {
            this.JoinPoint = null;
            this.Canceled = false;
            this.Exception = null;
            this.ExceptionHandled = false;
            this.Result = null;
        }

        public JoinPointDescriptor JoinPoint
        {
            get;
            set;
        }

        public bool Canceled
        {
            get;
            set;
        }

        public Exception Exception
        {
            get;
            set;
        }

        public bool ExceptionHandled
        {
            get;
            set;
        }

        public object Result
        {
            get;
            set;
        }
    }
}

namespace Squire.Unhinged.Aspects
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;

    public class BeforeExecutionContext
    {
        private bool resultChanged = false;

        private object result = null;

        public BeforeExecutionContext()
        {
            this.JoinPoint = null;
            this.Parameters = new Dictionary<string, object>(0);
            this.result = null;
        }

        public BeforeExecutionContext(JoinPointDescriptor joinPoint, IDictionary<string, object> parameters)
            : this()
        {
            this.JoinPoint = joinPoint;
            this.Parameters = parameters;
        }

        public BeforeExecutionContext(JoinPointDescriptor joinPoint)
            : this(joinPoint, null)
        {
            this.Parameters = this.JoinPoint.GetParameters().ToDictionary(p => p.ParameterName, p => p.DefaultValue);
        }

        public JoinPointDescriptor JoinPoint
        {
            get;
            set;
        }

        public IDictionary<string, object> Parameters
        {
            get;
            set;
        }

        public bool HasResult
        {
            get
            {
                if (this.JoinPoint != null)
                {
                    return !this.JoinPoint.HasVoidResult;
                }

                return false;
            }
        }

        public object Result
        {
            get
            {
                return this.result;
            }
            set
            {
                this.resultChanged = true;
                this.result = value;
            }
        }

        public bool IsResultSet
        {
            get
            {
                return this.resultChanged;
            }
        }
    }
}

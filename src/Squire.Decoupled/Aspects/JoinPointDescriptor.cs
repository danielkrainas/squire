namespace Squire.Decoupled.Aspects
{
    using Squire.Validation;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;

    public class JoinPointDescriptor
    {
        private readonly MethodInfo method;

        private readonly ParameterDescriptor[] parameters;

        public JoinPointDescriptor(MethodInfo method)
        {
            ValidationHelper.ArgumentNotNull(method, "method");
            this.method = method;
            this.parameters = this.method.GetParameters().Select(p => new ParameterDescriptor(this, p)).ToArray();
        }

        public string JointPointName
        {
            get
            {
                return this.method.Name;
            }
        }

        public MethodInfo MethodInfo
        {
            get
            {
                return this.method;
            }
        }

        public Type DeclaringType
        {
            get
            {
                return this.method.DeclaringType;
            }
        }

        public Type ResultType
        {
            get
            {
                return this.method.ReturnType;
            }
        }

        public bool HasVoidResult
        {
            get
            {
                return this.ResultType == typeof(void);
            }
        }

        public ParameterDescriptor[] GetParameters()
        {
            return this.parameters;
        }

        public object[] GetCustomAttributes(bool inherit)
        {
            return this.method.GetCustomAttributes(inherit);
        }

        public object[] GetCustomAttributes(Type attributeType, bool inherit)
        {
            return this.method.GetCustomAttributes(attributeType, inherit);
        }
    }
}

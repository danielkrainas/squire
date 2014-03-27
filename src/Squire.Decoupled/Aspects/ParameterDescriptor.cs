namespace Squire.Decoupled.Aspects
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;

    public class ParameterDescriptor
    {
        private readonly ParameterInfo parameter;

        private readonly JoinPointDescriptor joinPoint;

        public ParameterDescriptor(JoinPointDescriptor joinPoint, ParameterInfo parameter)
        {
            this.joinPoint = joinPoint;
            this.parameter = parameter;
        }

        public JoinPointDescriptor JoinPoint
        {
            get
            {
                return this.joinPoint;
            }
        }

        public string ParameterName
        {
            get
            {
                return this.parameter.Name;
            }
        }

        public object DefaultValue
        {
            get
            {
                return this.parameter.DefaultValue;
            }
        }

        public Type ParameterType
        {
            get
            {
                return this.parameter.ParameterType;
            }
        }

        public object[] GetCustomAttributes(bool inherit)
        {
            return this.parameter.GetCustomAttributes(inherit);
        }

        public object[] GetCustomAttributes(Type attributeType, bool inherit)
        {
            return this.parameter.GetCustomAttributes(attributeType, inherit);
        }
    }
}

namespace Squire.Validation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class ArgumentEmptyException : ArgumentException
    {
        public ArgumentEmptyException(string paramName, string message)
            : base(message, paramName)
        {
        }

        public ArgumentEmptyException(string paramName)
            : base("value cannot be empty", paramName)
        {
        }
    }
}

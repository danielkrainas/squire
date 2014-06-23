namespace Squire.Validation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public static class ArrayFluentExtensions
    {
        public static ValidationAnd<FluentArgument<T[]>> IsNotEmpty<T>(this FluentArgument<T[]> arg, string message = "")
        {
            if (arg.Target.Length <= 0)
            {
                if (string.IsNullOrWhiteSpace(message))
                {
                    throw new ArgumentEmptyException(arg.Name, "must contain at least one element");
                }
                else
                {
                    throw new ArgumentEmptyException(arg.Name, message);
                }
            }

            return new ValidationAnd<FluentArgument<T[]>>(arg);
        }
    }
}

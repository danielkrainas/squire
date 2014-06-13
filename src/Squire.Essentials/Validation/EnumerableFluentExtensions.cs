namespace Squire.Validation
{
    using System.Collections.Generic;
    using System.Linq;

    public static class EnumerableFluentExtensions
    {
        public static ValidationAnd<FluentArgument<IEnumerable<T>>> IsNotEmpty<T>(this FluentArgument<IEnumerable<T>> arg, string message = "")
        {
            if (!arg.Target.Any())
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

            return new ValidationAnd<FluentArgument<IEnumerable<T>>>(arg);
        }
    }
}

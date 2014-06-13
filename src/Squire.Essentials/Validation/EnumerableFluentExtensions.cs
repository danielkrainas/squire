namespace Squire.Validation
{
    using System.Collections.Generic;

    public static class EnumerableFluentExtensions
    {
        public static ValidationAnd<FluentArgument<IEnumerable<T>>> IsNotEmpty<T>(this FluentArgument<IEnumerable<T>> arg, string message = "")
        {
            ValidationHelper.ArgumentNotEmpty(arg.Target, arg.Name, message);
            return new ValidationAnd<FluentArgument<IEnumerable<T>>>(arg);
        }
    }
}

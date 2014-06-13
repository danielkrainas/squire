namespace Squire.Validation
{
    using System;

    public static class ComparableFluentExtensions
    {
        public static ValidationAnd<FluentArgument<T>> IsInRange<T>(this FluentArgument<T> arg, T min, T max, bool inclusive = true, string message = "")
            where T : IComparable
        {
            ValidationHelper.ArgumentInRange(arg.Target, arg.Name, min, max, inclusive, message);
            return new ValidationAnd<FluentArgument<T>>(arg);
        }

        public static ValidationAnd<FluentArgument<Nullable<T>>> IsInRange<T>(this FluentArgument<Nullable<T>> arg, T min, T max, bool inclusive = true, string message = "")
            where T : struct, IComparable
        {
            ValidationHelper.ArgumentInRange(arg.Target, arg.Name, min, max, inclusive, message);
            return new ValidationAnd<FluentArgument<T?>>(arg);
        }

        public static ValidationAnd<FluentArgument<T>> IsGreaterThan<T>(this FluentArgument<T> arg, T value, string message = "")
            where T : IComparable
        {
            ValidationHelper.ArgumentGreaterThan(arg.Target, arg.Name, value, message);
            return new ValidationAnd<FluentArgument<T>>(arg);
        }
    }
}

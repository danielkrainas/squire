namespace Squire.Validation
{
    using System;

    public static class ComparableFluentExtensions
    {
        public static ValidationAnd<FluentArgument<T>> IsInRange<T>(this FluentArgument<T> arg, T min, T max, bool inclusive = true, string message = "")
            where T : IComparable
        {
            if (inclusive && min.CompareTo(arg.Target) < 0 && max.CompareTo(arg.Target) > 0)
            {
                if (string.IsNullOrWhiteSpace(message))
                {
                    throw new ArgumentOutOfRangeException(arg.Name);
                }
                else
                {
                    throw new ArgumentOutOfRangeException(arg.Name, message);
                }
            }
            else if (min.CompareTo(arg.Target) <= 0 && max.CompareTo(arg.Target) >= 0)
            {
                if (string.IsNullOrWhiteSpace(message))
                {
                    throw new ArgumentOutOfRangeException(arg.Name);
                }
                else
                {
                    throw new ArgumentOutOfRangeException(arg.Name, message);
                }
            }
            return new ValidationAnd<FluentArgument<T>>(arg);
        }

        public static ValidationAnd<FluentArgument<Nullable<T>>> IsInRange<T>(this FluentArgument<Nullable<T>> arg, T min, T max, bool inclusive = true, string message = "")
            where T : struct, IComparable
        {
            var value = arg.Target.Value;
            if (inclusive && min.CompareTo(value) < 0 && max.CompareTo(value) > 0)
            {
                if (string.IsNullOrWhiteSpace(message))
                {
                    throw new ArgumentOutOfRangeException(arg.Name);
                }
                else
                {
                    throw new ArgumentOutOfRangeException(arg.Name, message);
                }
            }
            else if (min.CompareTo(value) <= 0 && max.CompareTo(value) >= 0)
            {
                if (string.IsNullOrWhiteSpace(message))
                {
                    throw new ArgumentOutOfRangeException(arg.Name);
                }
                else
                {
                    throw new ArgumentOutOfRangeException(arg.Name, message);
                }
            }

            return new ValidationAnd<FluentArgument<T?>>(arg);
        }

        public static ValidationAnd<FluentArgument<T>> IsGreaterThan<T>(this FluentArgument<T> arg, T value, string message = "")
            where T : IComparable
        {
            if (arg.Target.CompareTo(value) <= 0)
            {
                if (string.IsNullOrWhiteSpace(message))
                {
                    throw new ArgumentOutOfRangeException(arg.Name, string.Format("value({0}) must be greater than {1}", arg.Target, value));
                }
                else
                {
                    throw new ArgumentOutOfRangeException(arg.Name, message);
                }
            }

            return new ValidationAnd<FluentArgument<T>>(arg);
        }
    }
}

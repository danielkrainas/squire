namespace Squire.Validation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public static class FluentExtensions
    {
        public static FluentArgument<T> VerifyParam<T>(this T target, string name)
        {
            return new FluentArgument<T>(target, name);
        }

        public static ValidationAnd<FluentArgument<T>> IsNotNull<T>(this FluentArgument<T> arg, string message = "")
            where T : class
        {
            ValidationHelper.ArgumentNotNull(arg.Target, arg.Name, message);
            return new ValidationAnd<FluentArgument<T>>(arg);
        }

        public static ValidationAnd<FluentArgument<string>> IsNotEmpty(this FluentArgument<string> arg, string message = "")
        {
            ValidationHelper.ArgumentNotEmpty(arg.Target, arg.Name, message);
            return new ValidationAnd<FluentArgument<string>>(arg);
        }

        public static ValidationAnd<FluentArgument<IEnumerable<T>>> IsNotEmpty<T>(this FluentArgument<IEnumerable<T>> arg, string message = "")
        {
            ValidationHelper.ArgumentNotEmpty(arg.Target, arg.Name, message);
            return new ValidationAnd<FluentArgument<IEnumerable<T>>>(arg);
        }

        public static ValidationAnd<FluentArgument<Guid>> IsNotEmpty(this FluentArgument<Guid> arg, string message = "")
        {
            ValidationHelper.ArgumentNotEmpty(arg.Target, arg.Name, message);
            return new ValidationAnd<FluentArgument<Guid>>(arg);
        }

        public static ValidationAnd<FluentArgument<string>> IsNotWhiteSpace(this FluentArgument<string> arg, string message = "")
        {
            ValidationHelper.ArgumentNotWhiteSpace(arg.Target, arg.Name, message);
            return new ValidationAnd<FluentArgument<string>>(arg);
        }

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

        public static ValidationAnd<FluentArgument<int>> IsGreaterThanZero(this FluentArgument<int> arg, string message = "")
        {
            ValidationHelper.ArgumentGreaterThan(arg.Target, arg.Name, 0, message);
            return new ValidationAnd<FluentArgument<int>>(arg);
        }

        public static ValidationAnd<FluentArgument<int>> IsGreaterThan(this FluentArgument<int> arg, int value, string message = "")
        {
            ValidationHelper.ArgumentGreaterThan(arg.Target, arg.Name, value, message);
            return new ValidationAnd<FluentArgument<int>>(arg);
        }

        public static ValidationAnd<FluentArgument<string>> IsNotBlank(this FluentArgument<string> arg, string message = "")
        {
            ValidationHelper.ArgumentNotBlank(arg.Target, arg.Name, message);
            return new ValidationAnd<FluentArgument<string>>(arg);
        }

        public static ValidationAnd<FluentArgument<T>> IsGreaterThan<T>(this FluentArgument<T> arg, T value, string message = "")
            where T : IComparable
        {
            ValidationHelper.ArgumentGreaterThan(arg.Target, arg.Name, value, message);
            return new ValidationAnd<FluentArgument<T>>(arg);
        }

        public static ValidationAnd<FluentArgument<TGeneric>> IsSubClassOf<TGeneric, TSuperClass>(this FluentArgument<TGeneric> arg)
        {
            ValidationHelper.GenericTypeIsSubClassOf<TGeneric, TSuperClass>();
            return new ValidationAnd<FluentArgument<TGeneric>>(arg);
        }

        public static ValidationAnd<FluentArgument<T>> IsSubClassOf<T>(this FluentArgument<T> arg, Type superClass)
        {
            ValidationHelper.GenericTypeIsSubClassOf<T>(superClass);
            return new ValidationAnd<FluentArgument<T>>(arg);
        }
    }
}

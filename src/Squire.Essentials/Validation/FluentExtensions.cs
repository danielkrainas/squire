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

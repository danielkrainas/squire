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
            if (arg.Target == null)
            {
                if (string.IsNullOrWhiteSpace(message))
                {
                    throw new ArgumentNullException(arg.Name);
                }
                else
                {
                    throw new ArgumentNullException(arg.Name, message);
                }
            }

            return new ValidationAnd<FluentArgument<T>>(arg);
        }

        public static ValidationAnd<FluentArgument<TGeneric>> IsSubClassOf<TGeneric, TSuperClass>(this FluentArgument<TGeneric> arg)
        {
            return FluentExtensions.IsSubClassOf<TGeneric>(arg, typeof(TSuperClass));
        }

        public static ValidationAnd<FluentArgument<T>> IsSubClassOf<T>(this FluentArgument<T> arg, Type superClass)
        {
            superClass.VerifyParam("superClass").IsNotNull();
            if (!typeof(T).IsSubclassOf(superClass))
            {
                throw new InvalidOperationException(string.Format("'{0}' is not a subclass of type '{1}'.", typeof(T), superClass));
            }

            return new ValidationAnd<FluentArgument<T>>(arg);
        }
    }
}

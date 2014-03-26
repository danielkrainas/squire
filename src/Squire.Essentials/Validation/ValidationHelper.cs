namespace Squire.Validation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public static class ValidationHelper
    {
        public static void ArgumentNotEmpty(Guid guid, string name, string message = "")
        {
            if (guid == Guid.Empty)
            {
                if (string.IsNullOrWhiteSpace(message))
                {
                    throw new ArgumentEmptyException(name);
                }
                else
                {
                    throw new ArgumentEmptyException(name, message);
                }
            }
        }

        public static void ArgumentNotEmpty(string s, string name, string message = "")
        {
            if (string.IsNullOrEmpty(s))
            {
                if (string.IsNullOrWhiteSpace(message))
                {
                    throw new ArgumentEmptyException(name);
                }
                else
                {
                    throw new ArgumentEmptyException(name, message);
                }
            }
        }

        public static void ArgumentNotNull<T>(T obj, string name, string message = "")
            where T : class
        {
            if (obj == null)
            {
                if (string.IsNullOrWhiteSpace(message))
                {
                    throw new ArgumentNullException(name);
                }
                else
                {
                    throw new ArgumentNullException(name, message);
                }
            }
        }

        public static void GenericTypeIsSubClassOf<TGeneric>(Type superClass)
        {
            ValidationHelper.ArgumentNotNull(superClass, "superClass");
            if (!typeof(TGeneric).IsSubclassOf(superClass))
            {
                throw new InvalidOperationException(string.Format("'{0}' is not a subclass of type '{1}'.", typeof(TGeneric), superClass));
            }
        }

        public static void GenericTypeIsSubClassOf<TGeneric, TSuperClass>()
        {
            ValidationHelper.GenericTypeIsSubClassOf<TGeneric>(typeof(TSuperClass));
        }

        public static void ArgumentIsSubClassOf<T>(T argument, string argumentName, Type superclass, string message = "")
        {
            if (!typeof(T).IsSubclassOf(superclass))
            {
                if (string.IsNullOrWhiteSpace(message))
                {
                    message = string.Format("'{0}' is not a subclass of type '{1}'.", typeof(T), superclass);
                }

                throw new ArgumentException(message, argumentName);
            }
        }

        public static void TypeArgumentIsSubClassOf(Type argument, string argumentName, Type superclass, string message = "")
        {
            ValidationHelper.ArgumentNotNull(argument, "argument");
            ValidationHelper.ArgumentNotNull(argumentName, "argumentName");
            ValidationHelper.ArgumentNotNull(superclass, "superclass");
            if (!argument.IsSubclassOf(superclass))
            {
                if (string.IsNullOrWhiteSpace(message))
                {
                    message = string.Format("'{0}' is not a subclass of type '{1}'.", argument, superclass);
                }

                throw new ArgumentException(message, argumentName);
            }
        }

        public static void ArgumentInRange<T>(T target, string name, T min, T max, bool inclusive = true, string message = "")
            where T : IComparable
        {
            if (inclusive && min.CompareTo(target) < 0 && max.CompareTo(target) > 0)
            {
                if (string.IsNullOrWhiteSpace(message))
                {
                    throw new ArgumentOutOfRangeException(name);
                }
                else
                {
                    throw new ArgumentOutOfRangeException(name, message);
                }                
            }
            else if (min.CompareTo(target) <= 0 && max.CompareTo(target) >= 0)
            {
                if (string.IsNullOrWhiteSpace(message))
                {
                    throw new ArgumentOutOfRangeException(name);
                }
                else
                {
                    throw new ArgumentOutOfRangeException(name, message);
                }
            }
        }

        public static void ArgumentInRange<T>(Nullable<T> target, string name, T min, T max, bool inclusive = true, string message = "")
            where T : struct, IComparable
        {
            var value = target.Value;
            if (inclusive && min.CompareTo(value) < 0 && max.CompareTo(value) > 0)
            {
                if (string.IsNullOrWhiteSpace(message))
                {
                    throw new ArgumentOutOfRangeException(name);
                }
                else
                {
                    throw new ArgumentOutOfRangeException(name, message);
                }
            }
            else if (min.CompareTo(value) <= 0 && max.CompareTo(value) >= 0)
            {
                if (string.IsNullOrWhiteSpace(message))
                {
                    throw new ArgumentOutOfRangeException(name);
                }
                else
                {
                    throw new ArgumentOutOfRangeException(name, message);
                }
            }
        }

        public static void ArgumentNotWhiteSpace(string target, string name, string message = "")
        {
            if (string.IsNullOrWhiteSpace(target))
            {
                if (string.IsNullOrWhiteSpace(message))
                {
                    throw new ArgumentEmptyException(name, "value cannot contain only white space");
                }
                else
                {
                    throw new ArgumentEmptyException(name, message);
                }
            }
        }

        public static void ArgumentGreaterThan<T>(T target, string name, T value, string message = "")
            where T : IComparable
        {
            if (target.CompareTo(value) <= 0)
            {
                if (string.IsNullOrWhiteSpace(message))
                {
                    throw new ArgumentOutOfRangeException(name, string.Format("value({0}) must be greater than {1}", target, value));
                }
                else
                {
                    throw new ArgumentOutOfRangeException(name, message);
                }
            }
        }

        public static void ArgumentNotBlank(string target, string name, string message = "")
        {
            if (target == null)
            {
                if (string.IsNullOrWhiteSpace(message))
                {
                    throw new ArgumentNullException(name);
                }
                else
                {
                    throw new ArgumentNullException(name, message);
                }
            }

            if (string.IsNullOrWhiteSpace(target))
            {
                if (string.IsNullOrWhiteSpace(message))
                {
                    throw new ArgumentEmptyException(name);
                }
                else
                {
                    throw new ArgumentEmptyException(name, message);
                }
            }
        }

        public static void ArgumentNotEmpty<T>(IEnumerable<T> target, string name, string message = "")
        {
            if (!target.Any())
            {
                if (string.IsNullOrWhiteSpace(message))
                {
                    throw new ArgumentEmptyException(name, "must contain at least one element");
                }
                else
                {
                    throw new ArgumentEmptyException(name, message);
                }
            }
        }
    }
}

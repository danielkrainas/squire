namespace Squire.Validation
{
    using System;

    public static class StringFluentExtensions
    {
        public static ValidationAnd<FluentArgument<string>> IsNotEmpty(this FluentArgument<string> arg, string message = "")
        {
            if (string.IsNullOrEmpty(arg.Target))
            {
                if (string.IsNullOrWhiteSpace(message))
                {
                    throw new ArgumentEmptyException(arg.Name);
                }
                else
                {
                    throw new ArgumentEmptyException(arg.Name, message);
                }
            }

            return new ValidationAnd<FluentArgument<string>>(arg);
        }

        public static ValidationAnd<FluentArgument<string>> IsNotWhiteSpace(this FluentArgument<string> arg, string message = "")
        {
            if (string.IsNullOrWhiteSpace(arg.Target))
            {
                if (string.IsNullOrWhiteSpace(message))
                {
                    throw new ArgumentEmptyException(arg.Name, "value cannot contain only white space");
                }
                else
                {
                    throw new ArgumentEmptyException(arg.Name, message);
                }
            }

            return new ValidationAnd<FluentArgument<string>>(arg);
        }

        public static ValidationAnd<FluentArgument<string>> IsNotBlank(this FluentArgument<string> arg, string message = "")
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

            if (string.IsNullOrWhiteSpace(arg.Target))
            {
                if (string.IsNullOrWhiteSpace(message))
                {
                    throw new ArgumentEmptyException(arg.Name);
                }
                else
                {
                    throw new ArgumentEmptyException(arg.Name, message);
                }
            }

            return new ValidationAnd<FluentArgument<string>>(arg);
        }
    }
}

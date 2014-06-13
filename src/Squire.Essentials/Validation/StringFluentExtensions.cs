namespace Squire.Validation
{
    public static class StringFluentExtensions
    {
        public static ValidationAnd<FluentArgument<string>> IsNotEmpty(this FluentArgument<string> arg, string message = "")
        {
            ValidationHelper.ArgumentNotEmpty(arg.Target, arg.Name, message);
            return new ValidationAnd<FluentArgument<string>>(arg);
        }

        public static ValidationAnd<FluentArgument<string>> IsNotWhiteSpace(this FluentArgument<string> arg, string message = "")
        {
            ValidationHelper.ArgumentNotWhiteSpace(arg.Target, arg.Name, message);
            return new ValidationAnd<FluentArgument<string>>(arg);
        }

        public static ValidationAnd<FluentArgument<string>> IsNotBlank(this FluentArgument<string> arg, string message = "")
        {
            ValidationHelper.ArgumentNotBlank(arg.Target, arg.Name, message);
            return new ValidationAnd<FluentArgument<string>>(arg);
        }
    }
}

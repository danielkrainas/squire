namespace Squire.Validation
{
    public static class IntFluentExtensions
    {
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
    }
}

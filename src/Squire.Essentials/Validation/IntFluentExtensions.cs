namespace Squire.Validation
{
    public static class IntFluentExtensions
    {
        public static ValidationAnd<FluentArgument<int>> IsGreaterThanZero(this FluentArgument<int> arg, string message = "")
        {
            return IntFluentExtensions.IsGreaterThan(arg, 0, message);
        }

        public static ValidationAnd<FluentArgument<int>> IsGreaterThan(this FluentArgument<int> arg, int value, string message = "")
        {
            ComparableFluentExtensions.IsGreaterThan(arg, value, message);
            return new ValidationAnd<FluentArgument<int>>(arg);
        }
    }
}

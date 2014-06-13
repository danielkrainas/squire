namespace Squire.Validation
{
    using System;

    public static class GuidFluentExtensions
    {
        public static ValidationAnd<FluentArgument<Guid>> IsNotEmpty(this FluentArgument<Guid> arg, string message = "")
        {
            ValidationHelper.ArgumentNotEmpty(arg.Target, arg.Name, message);
            return new ValidationAnd<FluentArgument<Guid>>(arg);
        }
    }
}

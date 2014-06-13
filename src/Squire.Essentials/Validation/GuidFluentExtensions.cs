namespace Squire.Validation
{
    using System;

    public static class GuidFluentExtensions
    {
        public static ValidationAnd<FluentArgument<Guid>> IsNotEmpty(this FluentArgument<Guid> arg, string message = "")
        {
            if (arg.Target == Guid.Empty)
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

            return new ValidationAnd<FluentArgument<Guid>>(arg);
        }
    }
}

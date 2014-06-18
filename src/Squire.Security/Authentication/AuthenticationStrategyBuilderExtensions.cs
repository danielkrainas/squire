namespace Squire.Security.Authentication
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Squire.Validation;

    public static class AuthenticationStrategyBuilderExtensions
    {
        public static AuthenticationStrategyBuilder SimplePasswordValidation(this AuthenticationStrategyBuilder builder)
        {
            return builder.ValidateThrough(new SimplePasswordValidator());
        }

        public static AuthenticationStrategyBuilder ProtectWithMD5Hash(this AuthenticationStrategyBuilder builder)
        {
            return builder.ProtectPasswords(new MD5HashFilter());
        }

        public static AuthenticationStrategyBuilder ValidateBy(this AuthenticationStrategyBuilder builder, Func<IPlayer, string, bool> validator)
        {
            return builder.ValidateThrough(new InlineValidator(validator));
        }
    }
}

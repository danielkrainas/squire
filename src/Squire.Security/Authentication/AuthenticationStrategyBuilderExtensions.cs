namespace Squire.Security.Authentication
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

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
    }
}

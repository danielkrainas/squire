namespace Squire.Security.Authentication
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Squire.Validation;
    using System.Linq.Expressions;

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

        public static AuthenticationStrategyBuilder ValidateThrough(this AuthenticationStrategyBuilder builder, Func<IPlayer, string, bool> validator)
        {
            return builder.ValidateThrough(new InlineValidator(validator));
        }

        public static AuthenticationStrategyBuilder ValidateThrough(this AuthenticationStrategyBuilder builder, Expression<Func<IPlayer, string, bool>> validatorExpression)
        {
            return builder.ValidateThrough(new InlineValidator(validatorExpression));
        }

        public static AuthenticationStrategyBuilder ProtectPasswordsBy(this AuthenticationStrategyBuilder builder, Func<string, string, string> hashExpression)
        {
            return builder.ProtectPasswords(new InlineHashFilter(hashExpression));
        }

        public static AuthenticationStrategyBuilder ProtectPasswordsBy(this AuthenticationStrategyBuilder builder, Expression<Func<string, string, string>> hashExpression)
        {
            return builder.ProtectPasswords(new InlineHashFilter(hashExpression));
        }
    }
}

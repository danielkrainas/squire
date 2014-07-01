namespace Squire.Security
{
    using Squire.Security.Authentication;
    using Squire.Security.Authorization;
    using Squire.Setup;
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    public static class AppSetupSecurityExtensions
    {
        public static IAppSetup<T> EnableDevAuthentication<T>(this IAppSetup<T> setup, IPlayer authenticatedPlayer)
        {
            Authenticator.DevelopmentMode(authenticatedPlayer);
            return setup;
        }

        public static IAppSetup<T> EnableDevAuthorization<T>(this IAppSetup<T> setup, bool readOnly = false)
        {
            Authorizer.DevelopmentMode(readOnly);
            return setup;
        }

        public static IAppSetup<T> EnableDevAuthorization<T>(this IAppSetup<T> setup, IPlayer player, params string[] roleNames)
        {
            Authorizer.DevelopmentMode(false, player, roleNames);
            return setup;
        }

        public static IAppSetup<T> EnableDevReadOnlyAuthorization<T>(this IAppSetup<T> setup, IPlayer player, params string[] roleNames)
        {
            Authorizer.DevelopmentMode(true, player, roleNames);
            return setup;
        }

        public static IAppSetup<T> EnableDevAuthorization<T>(this IAppSetup<T> setup, bool readOnly, IDictionary<IPlayer, IEnumerable<string>> roleAssignments)
        {
            Authorizer.DevelopmentMode(readOnly, roleAssignments);
            return setup;
        }

        public static IAppSetup<T> EnableAuthorization<T>(this IAppSetup<T> setup, Expression<Func<AuthorizationStrategyBuilder, AuthorizationStrategyBuilder>> options)
        {
            Authorizer.BuildStrategy(options);
            return setup;
        }

        public static IAppSetup<T> EnableAuthentication<T>(this IAppSetup<T> setup, Expression<Func<AuthenticationStrategyBuilder, AuthenticationStrategyBuilder>> options)
        {
            Authenticator.BuildStrategy(options);
            return setup;
        }

        public static IAppSetup<T> EnableAuthentication<T>(this IAppSetup<T> setup, IAuthenticationStrategy provider)
        {
            Authenticator.Assign(provider);
            return setup;
        }

        public static IAppSetup<T> EnabledAuthorization<T>(this IAppSetup<T> setup, IAuthorizationStrategy provider)
        {
            Authorizer.Assign(provider);
            return setup;
        }
    }
}

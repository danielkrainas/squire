namespace Squire.Security.Authorization
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public static class AuthorizationStrategyBuilderExtensions
    {
        public static AuthorizationStrategyBuilder SimpleResolver(this AuthorizationStrategyBuilder builder)
        {
            return builder.Resolve(new SimpleRoleResolver());
        }

        public static AuthorizationStrategyBuilder AllowRegistrationWithCaching(this AuthorizationStrategyBuilder builder, IRoleRegistrar registrar)
        {
            return builder.AllowRegistration(new RoleRegistrarCacheFilter(registrar));
        }

        public static AuthorizationStrategyBuilder AllowRegistrationInMemory(this AuthorizationStrategyBuilder builder)
        {
            return builder.AllowRegistration(new InMemoryRoleRegistrar());
        }

        public static AuthorizationStrategyBuilder InMemoryTracker(this AuthorizationStrategyBuilder builder)
        {
            return builder.TrackWith(new InMemoryRoleTracker());
        }
    }
}

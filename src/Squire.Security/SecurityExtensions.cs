namespace Squire.Sentinel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Principal;
    using System.Text;

    public static class SecurityExtensions
    {
        public static IPlayer AsPlayer(this IIdentity identity)
        {
            return identity as IPlayer;
        }

        public static bool IsPlayer(this IIdentity identity)
        {
            return identity is IPlayer;
        }

        public static IPlayerPrincipal AsPlayerPrincipal(this IPrincipal principal)
        {
            return principal as IPlayerPrincipal;
        }
    }
}

namespace Squire.Security.Authorization
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public static class Authorizer
    {
        private static IAuthorizationStrategy _provider;

        public static void Assign(IAuthorizationStrategy provider)
        {
            Authorizer._provider = provider;
        }

        private static void VerifyReady()
        {
            if (Authorizer._provider == null)
            {
                throw new InvalidOperationException("the default provider has not been specified");
            }
        }

        public static void AddPlayer(IPlayer player, IRole role)
        {
            Authorizer.VerifyReady();
            Authorizer._provider.AddPlayer(player, role);
        }

        public static void RemovePlayer(IPlayer player, IRole role)
        {
            Authorizer.VerifyReady();
            Authorizer._provider.RemovePlayer(player, role);
        }

        public static bool IsIn(IPlayer player, IRole role)
        {
            Authorizer.VerifyReady();
            return Authorizer._provider.IsIn(player, role);
        }

        public static IRole Select(string roleId)
        {
            Authorizer.VerifyReady();
            return Authorizer._provider.Select(roleId);
        }

        public static IRole Register(RoleRegistration registration)
        {
            Authorizer.VerifyReady();
            return Authorizer.Register(registration);
        }

        public static void Unregister(IRole role)
        {
            Authorizer.VerifyReady();
            Authorizer.Unregister(role);
        }

        public static IEnumerable<IRole> AllRoles()
        {
            Authorizer.VerifyReady();
            return Authorizer._provider.AllRoles();
        }

        public static IEnumerable<IPlayer> PlayersIn(IRole role)
        {
            Authorizer.VerifyReady();
            return Authorizer._provider.PlayersIn(role);
        }
    }
}

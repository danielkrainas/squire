namespace Squire.Security.Authorization
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text;
    using Squire.Validation;

    public static class Authorizer
    {
        private static IAuthorizationStrategy _provider;

        public static void Assign(IAuthorizationStrategy provider)
        {
            Authorizer._provider = provider;
        }

        public static void BuildStrategy(Expression<Func<AuthorizationStrategyBuilder, AuthorizationStrategyBuilder>> buildExpression)
        {
            buildExpression.VerifyParam("buildExpression").IsNotNull();
            var builder = new AuthorizationStrategyBuilder();
            var strategy = buildExpression.Compile().Invoke(builder).Build();
            Authorizer.Assign(strategy);
        }

        public static void DevelopmentMode(bool readOnly = false)
        {
            Authorizer.Assign(new DevAuthorizationStrategy(readOnly));
        }

        public static void DevelopmentMode(bool readOnly, IPlayer player, params string[] roleNames)
        {
            Authorizer.Assign(new DevAuthorizationStrategy(readOnly, player, roleNames));
        }

        public static void DevelopmentMode(bool readOnly, IDictionary<IPlayer, IEnumerable<string>> roleAssignments)
        {
            Authorizer.Assign(new DevAuthorizationStrategy(readOnly, roleAssignments));
        }

        public static void DevelopmentMode(IPlayer player, params string[] roleNames)
        {
            Authorizer.DevelopmentMode(false, player, roleNames);
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

        public static IEnumerable<string> PlayersIn(IRole role)
        {
            Authorizer.VerifyReady();
            return Authorizer._provider.PlayersIn(role);
        }
    }
}

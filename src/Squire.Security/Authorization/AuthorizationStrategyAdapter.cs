namespace Squire.Security.Authorization
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Squire.Validation;

    public class AuthorizationStrategyAdapter : IAuthorizationStrategy
    {
        private readonly IRoleRegistrar registrar;

        private readonly IRoleResolver resolver;

        private readonly IRoleTracker tracker;

        public AuthorizationStrategyAdapter(IRoleResolver resolver, IRoleTracker tracker, IRoleRegistrar registrar = null)
        {
            resolver.VerifyParam("resolver").IsNotNull();
            tracker.VerifyParam("tracker").IsNotNull();
            this.resolver = resolver;
            this.tracker = tracker;
            this.registrar = registrar;
        }

        public bool IsIn(IPlayer player, IRole role)
        {
            player.VerifyParam("player").IsNotNull();
            role.VerifyParam("role").IsNotNull();
            return this.tracker.IsFamiliar(role, player);
        }

        public IEnumerable<IRole> AllRoles()
        {
            return this.resolver.ResolveAll();
        }

        public IEnumerable<IPlayer> PlayersIn(IRole role)
        {
            role.VerifyParam("role").IsNotNull();
            return this.tracker.GetPlayers(role);
        }

        public IRole Select(string id)
        {
            id.VerifyParam("id").IsNotNull();
            return this.resolver.ResolveAll().FirstOrDefault(r => r.Id.Equals(id));
        }

        public IRole Register(RoleRegistration registration)
        {
            registration.VerifyParam("registration").IsNotNull();
            if (this.registrar == null)
            {
                throw new InvalidOperationException("registrar not specified for this authorization strategy");
            }

            var role = this.resolver.Resolve(registration);
            this.registrar.Register(role);
            return role;
        }

        public void Unregister(IRole role)
        {
            role.VerifyParam("role").IsNotNull();
            if (this.registrar == null)
            {
                throw new InvalidOperationException("registrar not specified for this authorization strategy");
            }

            this.registrar.Unregister(role);
        }

        public void AddPlayer(IPlayer player, IRole role)
        {
            player.VerifyParam("player").IsNotNull();
            role.VerifyParam("role").IsNotNull();
            this.tracker.Remember(role, player);
        }

        public void RemovePlayer(IPlayer player, IRole role)
        {
            player.VerifyParam("player").IsNotNull();
            role.VerifyParam("role").IsNotNull();
            this.tracker.Forget(role, player);
        }
    }
}

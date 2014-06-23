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

        protected bool IsRegisteredSafe(IRole role)
        {
            return this.registrar == null || (this.registrar != null && this.registrar.IsRegistered(role));
        }

        public virtual bool IsIn(IPlayer player, IRole role)
        {
            player.VerifyParam("player").IsNotNull();
            role.VerifyParam("role").IsNotNull();
            if (this.IsRegisteredSafe(role))
            {
                return this.tracker.IsFamiliar(role, player);
            }

            return false;
        }

        public virtual IEnumerable<IRole> AllRoles()
        {
            return this.resolver.ResolveAll();
        }

        public virtual IEnumerable<IPlayer> PlayersIn(IRole role)
        {
            role.VerifyParam("role").IsNotNull();
            if (this.IsRegisteredSafe(role))
            {
                return this.tracker.GetPlayers(role);
            }

            return Enumerable.Empty<IPlayer>();
        }

        public virtual IRole Select(string id)
        {
            id.VerifyParam("id").IsNotNull();
            return this.resolver.Resolve(id);
        }

        public virtual IRole Register(RoleRegistration registration)
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

        public virtual void Unregister(IRole role)
        {
            role.VerifyParam("role").IsNotNull();
            if (this.registrar == null)
            {
                throw new InvalidOperationException("registrar not specified for this authorization strategy");
            }

            if (this.registrar.IsRegistered(role))
            {
                this.tracker.Forget(role);
                this.registrar.Unregister(role);
            }
        }

        public virtual void AddPlayer(IPlayer player, IRole role)
        {
            player.VerifyParam("player").IsNotNull();
            role.VerifyParam("role").IsNotNull();
            if (this.IsRegisteredSafe(role))
            {
                this.tracker.Remember(role, player);
            }
        }

        public virtual void RemovePlayer(IPlayer player, IRole role)
        {
            player.VerifyParam("player").IsNotNull();
            role.VerifyParam("role").IsNotNull();
            if (this.IsRegisteredSafe(role))
            {
                this.tracker.Forget(role, player);
            }
        }

        public virtual void RemovePlayer(IPlayer player)
        {
            player.VerifyParam("player").IsNotNull();
            this.tracker.Forget(player);
        }
    }
}

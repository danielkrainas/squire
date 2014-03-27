namespace Squire.Sentinel.Authentication
{
    using Squire.Decoupled.Pipeline;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class AuthenticationStrategyBuilder
    {
        private IHashFilter hash;

        private IPlayerResolver playerResolver;

        private IPlayerRegistrar registrar;

        private IValidator validator;

        private ISessionTracker tracker;

        private IRoleResolver roleResolver;

        public AuthenticationStrategyBuilder()
        {
            this.hash = null;
            this.registrar = null;
            this.validator = null;
            this.tracker = null;
            this.roleResolver = null;
        }

        public AuthenticationStrategyBuilder ProtectPasswords(IHashFilter hash)
        {
            this.hash = hash;
            return this;
        }

        public AuthenticationStrategyBuilder ResolvePlayersBy(IPlayerResolver resolver)
        {
            this.playerResolver = resolver;
            return this;
        }

        public AuthenticationStrategyBuilder ResolveRolesBy(IRoleResolver resolver)
        {
            this.roleResolver = resolver;
            return this;
        }

        public AuthenticationStrategyBuilder AllowRegistration(IPlayerRegistrar registrar)
        {
            this.registrar = registrar;
            return this;
        }

        public AuthenticationStrategyBuilder ValidateThrough(IValidator validator)
        {
            this.validator = validator;
            return this;
        }

        public AuthenticationStrategyBuilder TrackWith(ISessionTracker tracker)
        {
            this.tracker = tracker;
            return this;
        }

        public AuthenticationStrategyAdapter Build()
        {
            if (this.playerResolver == null)
            {
                throw new InvalidOperationException("you must specify a player resolver");
            }

            if (this.validator == null)
            {
                throw new InvalidOperationException("you must specify a validator");
            }

            if (this.tracker == null)
            {
                throw new InvalidOperationException("you must specify a session tracker");
            }

            return new AuthenticationStrategyAdapter(this.playerResolver, this.validator, this.tracker, hash: this.hash, registrar: this.registrar, roleResolver: this.roleResolver);
        }
    }
}

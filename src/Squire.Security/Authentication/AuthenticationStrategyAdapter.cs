namespace Squire.Security.Authentication
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Squire.Validation;

    public class AuthenticationStrategyAdapter : IAuthenticationStrategy
    {
        private readonly IPlayerResolver playerResolver;

        private readonly IValidator validator;

        private readonly IHashFilter hash;

        private readonly ISessionTracker sessionManager;

        private readonly IRoleResolver roleResolver;

        private readonly IPlayerRegistrar registrar;

        public AuthenticationStrategyAdapter(IPlayerResolver playerResolver, IValidator validator, ISessionTracker sessionManager, IHashFilter hash = null, IRoleResolver roleResolver = null, IPlayerRegistrar registrar = null)
        {
            playerResolver.VerifyParam("playerResolver").IsNotNull();
            validator.VerifyParam("validator").IsNotNull();
            sessionManager.VerifyParam("sessionManager").IsNotNull();
            this.playerResolver = playerResolver;
            this.validator = validator;
            this.hash = hash;
            this.sessionManager = sessionManager;
            this.roleResolver = roleResolver;
            this.registrar = registrar;
        }

        public bool IsPasswordHashEnabled
        {
            get
            {
                return this.hash != null;
            }
        }

        public bool SupportsRegistration
        {
            get
            {
                return this.registrar != null;
            }
        }

        public IPlayer Validate(string name, string password)
        {
            var player = this.playerResolver.Resolve(name);
            if(player != null)
            {
                if (this.IsPasswordHashEnabled)
                {
                    password = this.hash.Calculate(player.Name, password);
                }

                if (!this.validator.Validate(player, password))
                {
                    player = null;
                }
            }

            return player;
        }

        public void StartSession(IPlayer player, bool persist)
        {
            this.sessionManager.Start(player, Enumerable.Empty<string>(), persist);
        }

        public void ResumeSession()
        {
            this.sessionManager.Restore();
        }

        public void EndSession()
        {
            this.sessionManager.End();
        }

        public IPlayer Register(RegistrationDetails registration)
        {
            registration.VerifyParam("registration").IsNotNull();
            if(this.IsPasswordHashEnabled)
            {
                registration.Password = this.hash.Calculate(registration.Name, registration.Password);
            }

            var player = this.playerResolver.Resolve(registration);
            if (player != null)
            {
                this.registrar.Register(player);
            }

            return player;
        }
    }
}

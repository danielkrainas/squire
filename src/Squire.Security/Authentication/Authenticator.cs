namespace Squire.Sentinel.Authentication
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Squire.Validation;
    using System.Threading;

    public static class Authenticator
    {
        private static IAuthenticationStrategy _provider;

        public static void Assign(IAuthenticationStrategy provider)
        {
            Authenticator._provider = provider;
        }

        public static void DevelopmentMode(IPlayer authenticatedPlayer)
        {
            Authenticator.Assign(new DevAuthenticationStrategy(authenticatedPlayer));
        }

        private static void VerifyReady()
        {
            if (Authenticator._provider == null)
            {
                throw new InvalidOperationException("the default provider has not been specified");
            }
        }

        public static IPlayer Validate(string name, string password)
        {
            Authenticator.VerifyReady();
            return Authenticator._provider.Validate(name, password);
        }

        public static void StartSession(IPlayer player, bool persist)
        {
            Authenticator.VerifyReady();
            Authenticator._provider.StartSession(player, persist);
        }

        public static void ResumeSession()
        {
            Authenticator.VerifyReady();
            Authenticator._provider.ResumeSession();
        }

        public static void EndSession()
        {
            Authenticator.VerifyReady();
            Authenticator._provider.EndSession();
        }

        public static IPlayer Register(RegistrationDetails registration)
        {
            Authenticator.VerifyReady();
            return Authenticator._provider.Register(registration);
        }

        public static IPlayerPrincipal CurrentPlayer
        {
            get
            {
                return Thread.CurrentPrincipal as IPlayerPrincipal;
            }
        }
    }
}

namespace Squire.Security.Authentication
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    public class DevAuthenticationStrategy : IAuthenticationStrategy
    {
        private readonly IPlayer authenticatedPlayer;

        private readonly IPlayer guestPlayer;

        public DevAuthenticationStrategy(IPlayer authenticatedPlayer)
        {
            this.authenticatedPlayer = authenticatedPlayer;
            this.guestPlayer = new GuestPlayer();
            this.MakePrimary(this.guestPlayer);
        }

        private void MakePrimary(IPlayer player)
        {
            Thread.CurrentPrincipal = new PlayerPrincipal(player, Enumerable.Empty<string>());
        }

        public IPlayer Validate(string name, string password)
        {
            if (name.Equals(this.authenticatedPlayer.Name, StringComparison.InvariantCultureIgnoreCase))
            {
                return this.authenticatedPlayer;
            }

            return null;
        }

        public void StartSession(IPlayer player, bool persist)
        {
            this.MakePrimary(this.authenticatedPlayer);
        }

        public void ResumeSession()
        {
            //this.MakePrimary(this.authenticatedPlayer);
        }

        public void EndSession()
        {
            this.MakePrimary(this.guestPlayer);
        }

        public IPlayer Register(RegistrationDetails registration)
        {
            return null;
        }
    }
}

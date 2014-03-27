namespace Squire.Sentinel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Squire.Validation;
    using Squire.Decoupled.DomainEvents;
    using Squire.Sentinel.Authentication;
    using System.Security.Principal;
    using Squire.Sentinel.Abilities;

    public class PlayerPrincipal : IPlayerPrincipal
    {
        private readonly List<string> roles;

        private PlayerPrincipal()
        {
            this.roles = new List<string>();
        }

        public PlayerPrincipal(IPlayer player, IEnumerable<string> roles)
            : this()
        {
            player.VerifyParam("player").IsNotNull();
            roles.VerifyParam("roles").IsNotNull();
            this.Identity = player;
            this.roles.AddRange(roles);
        }

        public IPlayer Identity
        {
            get;
            private set;
        }

        IIdentity IPrincipal.Identity
        {
            get
            {
                return this.Identity;
            }
        }

        public bool IsGuest
        {
            get
            {
                return this.Identity is GuestPlayer;
            }
        }

        public bool IsInRole(string role)
        {
            if (this.IsGuest)
            {
                return false;
            }

            return this.roles.Contains(role);
        }

        public bool CheckAbility<TAbility>()
            where TAbility : class, IAbility
        {
            return Ability.Check<TAbility>(this.Identity);
        }
    }
}

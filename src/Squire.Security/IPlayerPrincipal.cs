namespace Squire.Security
{
    using Squire.Security.Abilities;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Principal;
    using System.Text;

    public interface IPlayerPrincipal : IPrincipal
    {
        new IPlayer Identity
        {
            get;
        }

        bool IsGuest
        {
            get;
        }

        bool CheckAbility<TAbility>()
            where TAbility : class, IAbility;
    }
}

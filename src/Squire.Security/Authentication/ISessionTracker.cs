namespace Squire.Security.Authentication
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public interface ISessionTracker
    {
        IPlayerPrincipal Restore();

        IPlayerPrincipal Start(IPlayer player, IEnumerable<string> roles, bool persist);

        void End();
    }
}

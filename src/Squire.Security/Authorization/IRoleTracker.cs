namespace Squire.Security.Authorization
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public interface IRoleTracker
    {
        IEnumerable<IRole> GetRoles(IPlayer player);

        IEnumerable<IPlayer> GetPlayers(IRole role);

        bool IsFamiliar(IRole role, IPlayer player);

        void Remember(IRole role, IPlayer player);

        void Forget(IRole role, IPlayer player);

        void Forget(IPlayer player);

        void Forget(IRole role);
    }
}

namespace Squire.Security.Authorization
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public interface IAuthorizationStrategy
    {
        bool IsIn(IPlayer player, IRole role);

        IEnumerable<IRole> AllRoles();

        IEnumerable<IPlayer> PlayersIn(IRole role);

        IRole Select(string id);

        IRole Register(RoleRegistration registration);

        void Unregister(IRole role);

        void AddPlayer(IPlayer player, IRole role);

        void RemovePlayer(IPlayer player, IRole role);

        void RemovePlayer(IPlayer player);
    }
}

namespace Squire.Security.Authorization
{
    using System.Collections.Generic;

    public interface IAuthorizationStrategy
    {
        bool IsIn(IPlayer player, IRole role);

        IEnumerable<IRole> AllRoles();

        IEnumerable<string> PlayersIn(IRole role);

        IRole Select(string id);

        IRole Register(RoleRegistration registration);

        void Unregister(IRole role);

        void AddPlayer(IPlayer player, IRole role);

        void RemovePlayer(IPlayer player, IRole role);

        void RemovePlayer(IPlayer player);
    }
}

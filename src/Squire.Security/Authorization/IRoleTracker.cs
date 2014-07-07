namespace Squire.Security.Authorization
{
    using System.Collections.Generic;

    public interface IRoleTracker
    {
        /// <summary>
        /// Gets roles associated with a player, specified by <paramref name="player"/>.
        /// </summary>
        /// <param name="player">A player</param>
        /// <returns>Returns an enumerable of role id&apos;s.</returns>
        IEnumerable<string> GetRoles(IPlayer player);

        /// <summary>
        /// Gets players associated with a role, specified by <paramref name="role"/>.
        /// </summary>
        /// <param name="role">A role</param>
        /// <returns>Returns an enumerable of player id&apos;s.</returns>
        IEnumerable<string> GetPlayers(IRole role);

        bool IsFamiliar(IRole role, IPlayer player);

        void Remember(IRole role, IPlayer player);

        void Forget(IRole role, IPlayer player);

        void Forget(IPlayer player);

        void Forget(IRole role);
    }
}

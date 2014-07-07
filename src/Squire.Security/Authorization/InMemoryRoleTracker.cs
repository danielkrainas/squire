namespace Squire.Security.Authorization
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Squire.Validation;

    public class InMemoryRoleTracker : IRoleTracker
    {
        private readonly Dictionary<IRole, List<IPlayer>> roleLookup;

        private readonly Dictionary<IPlayer, List<IRole>> playerLookup;

        public InMemoryRoleTracker()
        {
            this.roleLookup = new Dictionary<IRole, List<IPlayer>>();
            this.playerLookup = new Dictionary<IPlayer, List<IRole>>();
        }

        /// <summary>
        /// Gets roles associated with a player, specified by <paramref name="player"/>.
        /// </summary>
        /// <param name="player">A player</param>
        /// <returns>Returns an enumerable of role id&apos;s.</returns>
        public IEnumerable<string> GetRoles(IPlayer player)
        {
            player.VerifyParam("player").IsNotNull();
            if(this.playerLookup.ContainsKey(player))
            {
                return this.playerLookup[player].Select(r => r.Id);
            }

            return Enumerable.Empty<string>();
        }

        /// <summary>
        /// Gets players associated with a role, specified by <paramref name="role"/>.
        /// </summary>
        /// <param name="role">A role</param>
        /// <returns>Returns an enumerable of player id&apos;s.</returns>
        public IEnumerable<string> GetPlayers(IRole role)
        {
            role.VerifyParam("role").IsNotNull();
            if (this.roleLookup.ContainsKey(role))
            {
                return this.roleLookup[role].Select(p => p.Id);
            }

            return Enumerable.Empty<string>();
        }

        public bool IsFamiliar(IRole role, IPlayer player)
        {
            role.VerifyParam("role").IsNotNull();
            player.VerifyParam("player").IsNotNull();
            return this.GetRoles(player).Any(r => r.Equals(role.Id));
        }

        public void Remember(IRole role, IPlayer player)
        {
            if (!this.roleLookup.ContainsKey(role))
            {
                this.roleLookup.Add(role, new List<IPlayer>());
            }

            var rolePlayers = this.roleLookup[role];
            if (!rolePlayers.Contains(player))
            {
                rolePlayers.Add(player);
            }

            if (!this.playerLookup.ContainsKey(player))
            {
                this.playerLookup.Add(player, new List<IRole>());
            }

            var playerRoles = this.playerLookup[player];
            if (!playerRoles.Contains(role))
            {
                playerRoles.Add(role);
            }
        }

        public void Forget(IRole role, IPlayer player)
        {
            throw new NotImplementedException();
        }

        public void Forget(IPlayer player)
        {
            player.VerifyParam("player").IsNotNull();
            if (this.playerLookup.ContainsKey(player))
            {
                var roles = this.playerLookup[player];
                foreach (var r in roles.Where(r => this.roleLookup.ContainsKey(r)))
                {
                    this.roleLookup[r].Remove(player);
                }

                this.playerLookup.Remove(player);
            }
        }

        public void Forget(IRole role)
        {
            role.VerifyParam("role").IsNotNull();
            if (this.roleLookup.ContainsKey(role))
            {
                var players = this.roleLookup[role];
                foreach (var p in players.Where(p => this.playerLookup.ContainsKey(p)))
                {
                    this.playerLookup[p].Remove(role);
                }

                this.roleLookup.Remove(role);
            }
        }
    }
}

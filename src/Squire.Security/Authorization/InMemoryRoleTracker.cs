namespace Squire.Security.Authorization
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Squire.Validation;

    public class InMemoryRoleTracker : IRoleTracker
    {
        private Dictionary<IRole, List<IPlayer>> roleLookup;

        private Dictionary<IPlayer, List<IRole>> playerLookup;

        public InMemoryRoleTracker()
        {

        }

        public IEnumerable<IRole> GetRoles(IPlayer player)
        {
            player.VerifyParam("player").IsNotNull();
            if(this.playerLookup.ContainsKey(player))
            {
                return this.playerLookup[player];
            }

            return Enumerable.Empty<IRole>();
        }

        public IEnumerable<IPlayer> GetPlayers(IRole role)
        {
            role.VerifyParam("role").IsNotNull();
            if (this.roleLookup.ContainsKey(role))
            {
                return this.roleLookup[role];
            }

            return Enumerable.Empty<IPlayer>();
        }

        public bool IsFamiliar(IRole role, IPlayer player)
        {
            role.VerifyParam("role").IsNotNull();
            player.VerifyParam("player").IsNotNull();
            var roles = this.GetRoles(player);
            return roles.Contains(role);
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

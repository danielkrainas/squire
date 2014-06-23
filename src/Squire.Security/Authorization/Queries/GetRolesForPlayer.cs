namespace Squire.Security.Authorization.Queries
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Squire.Decoupled.Queries;
    using Squire.Validation;

    public class GetRolesForPlayer : IQuery<ICollection<IRole>>
    {
        public GetRolesForPlayer(IPlayer player)
        {
            player.VerifyParam("player").IsNotNull();
            this.Player = player;
        }

        public IPlayer Player
        {
            get;
            private set;
        }
    }
}

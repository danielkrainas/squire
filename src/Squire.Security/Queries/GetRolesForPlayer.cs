namespace Squire.Sentinel.Queries
{
    using Squire.Unhinged.Queries;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Squire.Validation;

    public class GetRolesForPlayer : IQuery<ICollection<string>>
    {
        public GetRolesForPlayer(string playerId)
        {
            playerId.VerifyParam("playerId").IsNotNull();
            this.PlayerId = playerId;
        }

        public string PlayerId
        {
            get;
            private set;
        }
    }
}

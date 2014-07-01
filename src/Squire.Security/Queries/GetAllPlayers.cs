namespace Squire.Security.Queries
{
    using Squire.Decoupled.Queries;
    using System.Collections.Generic;

    public class GetAllPlayers : IQuery<ICollection<IPlayer>>
    {
        public GetAllPlayers()
        {
        }
    }
}

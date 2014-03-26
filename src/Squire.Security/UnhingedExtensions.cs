namespace Squire.Sentinel
{
    using Squire.Sentinel.Queries;
    using Squire.Unhinged.Queries;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public static class UnhingedExtensions
    {
        public static IPlayer GetPlayerById(this IDispatchQuery dispatch, string id)
        {
            return dispatch.Execute(new GetPlayerById(id));
        }

        public static IPlayer GetPlayerByName(this IDispatchQuery dispatch, string name)
        {
            return dispatch.Execute(new GetPlayerByName(name));
        }

        public static ICollection<string> GetRolesForPlayer(this IDispatchQuery dispatch, string playerId)
        {
            return dispatch.Execute(new GetRolesForPlayer(playerId));
        }
    }
}

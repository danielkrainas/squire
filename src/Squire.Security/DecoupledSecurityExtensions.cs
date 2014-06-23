namespace Squire.Security
{
    using Squire.Security.Queries;
    using Squire.Decoupled.Queries;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public static class DecoupledSecurityExtensions
    {
        public static IPlayer GetPlayerById(this IDispatchQuery dispatch, string id)
        {
            return dispatch.Execute(new GetPlayerById(id));
        }

        public static IPlayer GetPlayerByName(this IDispatchQuery dispatch, string name)
        {
            return dispatch.Execute(new GetPlayerByName(name));
        }
    }
}

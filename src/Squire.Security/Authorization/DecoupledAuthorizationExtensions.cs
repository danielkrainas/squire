namespace Squire.Security.Authorization
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Squire.Decoupled.Queries;
    using Squire.Security.Authorization.Queries;

    public static class DecoupledAuthorizationExtensions
    {
        public static ICollection<IRole> GetRolesForPlayer(this IDispatchQuery dispatch, IPlayer player)
        {
            return dispatch.Execute(new GetRolesForPlayer(player));
        }
    }
}

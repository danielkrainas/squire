namespace Squire.Security
{
    using Squire.Security.Queries;
    using Squire.Decoupled.Queries;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Squire.Validation;

    public static class DecoupledSecurityExtensions
    {
        public static IPlayer GetPlayerById(this IDispatchQuery dispatch, string id)
        {
            dispatch.VerifyParam("dispatch").IsNotNull();
            return dispatch.Execute(new GetPlayerById(id));
        }

        public static IPlayer GetPlayerByName(this IDispatchQuery dispatch, string name)
        {
            dispatch.VerifyParam("dispatch").IsNotNull();
            return dispatch.Execute(new GetPlayerByName(name));
        }

        public static IPlayer GetPlayerByEmail(this IDispatchQuery dispatch, string email)
        {
            dispatch.VerifyParam("dispatch").IsNotNull();
            return dispatch.Execute(new GetPlayerByEmail(email));
        }

        public static ICollection<IPlayer> GetAllPlayers(this IDispatchQuery dispatch)
        {
            dispatch.VerifyParam("dispatch").IsNotNull();
            return dispatch.Execute(new GetAllPlayers());
        }

        public static ICollection<IPlayer> GetPlayers(this IDispatchQuery dispatch, int pageIndex, int pageSize, string emailFilter = null, string nameFilter = null)
        {
            dispatch.VerifyParam("dispatch").IsNotNull();
            return dispatch.Execute(new GetPlayers().WithEmailLike(emailFilter).WithNameLike(nameFilter).Page(pageIndex, pageSize));
        }
    }
}

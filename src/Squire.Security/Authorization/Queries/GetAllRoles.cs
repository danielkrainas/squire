namespace Squire.Security.Authorization.Queries
{
    using Squire.Decoupled.Queries;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class GetAllRoles : IQuery<ICollection<IRole>>
    {
        public GetAllRoles()
        {
        }
    }
}

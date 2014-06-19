namespace Squire.Security.Authorization
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public interface IRoleResolver
    {
        IEnumerable<IRole> ResolveAll();

        IRole Resolve(string id);

        IRole Resolve(RoleRegistration registration);
    }
}

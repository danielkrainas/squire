namespace Squire.Security.Authorization
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Squire.Validation;

    public static class RoleEnumerationExtensions
    {
        public static IEnumerable<string> SelectRoleNames(this IEnumerable<IRole> roles)
        {
            roles.VerifyParam("roles").IsNotNull();
            return roles.Select(r => r.Name);
        }
    }
}

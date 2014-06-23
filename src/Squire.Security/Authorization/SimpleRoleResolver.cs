namespace Squire.Security.Authorization
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class SimpleRoleResolver : IRoleResolver
    {
        private readonly Dictionary<string, SimpleRole> roles;

        public SimpleRoleResolver()
        {
            this.roles = new Dictionary<string, SimpleRole>();
        }

        public IEnumerable<IRole> ResolveAll()
        {
            return this.roles.Values;
        }

        public IRole Resolve(string id)
        {
            IRole role = null;
            if (!this.roles.ContainsKey(id))
            {
                role = this.Resolve(new RoleRegistration(id));
            }
            else
            {
                role = this.roles[id];
            }

            return role;
        }

        public IRole Resolve(RoleRegistration registration)
        {
            var role = new SimpleRole(registration.Name);
            if(this.roles.ContainsKey(role.Id))
            {
                role = this.roles[role.Id];
            }

            this.roles.Add(role.Id, role);
            return role;
        }
    }
}

namespace Squire.Security.Authorization
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Squire.Validation;

    public class SimpleRoleResolver : IRoleResolver
    {
        private readonly Dictionary<string, SimpleRole> roles;

        public SimpleRoleResolver()
        {
            this.roles = new Dictionary<string, SimpleRole>();
        }

        /// <summary>
        /// Resolves all roles.
        /// </summary>
        /// <returns>Returns an enumeration of all roles available.</returns>
        public IEnumerable<IRole> ResolveAll()
        {
            return this.roles.Values;
        }

        /// <summary>
        /// Resolves all roles that match the given ids, specified by <paramref name="ids"/>.
        /// </summary>
        /// <param name="ids">Ids of the roles to resolve</param>
        /// <returns>Returns an enumeration of all roles that were matched, otherwise an empty set.</returns>
        public IEnumerable<IRole> ResolveAll(params string[] ids)
        {
            ids.VerifyParam("ids").IsNotNull()
                .And.IsNotEmpty();

            return ids.Select(id => this.Resolve(id));
        }

        /// <summary>
        /// Resolves a role by its id, specified by <paramref name="id"/>.
        /// </summary>
        /// <param name="id">Id of the role to resolve</param>
        /// <returns>Returns the role if found, otherwise null.</returns>
        public IRole Resolve(string id)
        {
            id.VerifyParam("id").IsNotNull();
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

        /// <summary>
        /// Resolves a role based on a registration ticket, specified by <paramref name="registration"/>.
        /// </summary>
        /// <param name="registration">Registration ticket for the role to resolve.</param>
        /// <returns>Returns the a newly created role if the ticket was valid, otherwise null.</returns>
        public IRole Resolve(RoleRegistration registration)
        {
            registration.VerifyParam("registration").IsNotNull();
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

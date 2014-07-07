namespace Squire.Security.Authorization
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Squire.Validation;

    /// <summary>
    /// A cache-enabled role resolver. 
    /// </summary>
    public class RoleCacheResolveFilter : IRoleResolver
    {
        private readonly IRoleCacher cacher;

        private readonly IRoleResolver resolver;

        public RoleCacheResolveFilter(IRoleCacher cacher, IRoleResolver innerResolver)
        {
            cacher.VerifyParam("cacher").IsNotNull();
            innerResolver.VerifyParam("innerResolver").IsNotNull();
            this.cacher = cacher;
            this.resolver = innerResolver;
            this.cacher.InvalidateAll();
        }

        /// <summary>
        /// Resolves all roles.
        /// </summary>
        /// <returns>Returns an enumeration of all roles available.</returns>
        public IEnumerable<IRole> ResolveAll()
        {
            if (this.cacher.SupportsAll && this.cacher.HasAllCached)
            {
                return this.cacher.RetrieveAll();
            }

            var all = this.resolver.ResolveAll();
            if (all != null && this.cacher.SupportsAll)
            {
                this.cacher.PushAll(all);
            }

            return all;
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

            var found = new List<IRole>();
            var unfound = new List<string>();
            foreach (var id in ids)
            {
                var role = this.cacher.Retrieve(id);
                if (role == null)
                {
                    unfound.Add(id);
                }
                else
                {
                    found.Add(role);
                }
            }

            if (unfound.Any())
            {
                foreach(var role in this.resolver.ResolveAll(unfound.ToArray()))
                {
                    this.cacher.Push(role.Id, role);
                    found.Add(role);
                }
            }

            return found;
        }

        /// <summary>
        /// Resolves a role by its id, specified by <paramref name="id"/>.
        /// </summary>
        /// <param name="id">Id of the role to resolve</param>
        /// <returns>Returns the role if found, otherwise null.</returns>
        public IRole Resolve(string id)
        {
            var role = this.cacher.Retrieve(id);
            if (role != null)
            {
                return role;
            }

            role = this.resolver.Resolve(id);
            if (role != null)
            {
                this.cacher.Push(id, role);
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
            return this.resolver.Resolve(registration);
        }
    }
}

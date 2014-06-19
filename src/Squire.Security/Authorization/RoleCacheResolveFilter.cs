namespace Squire.Security.Authorization
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Squire.Validation;

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

        public IRole Resolve(RoleRegistration registration)
        {
            return this.resolver.Resolve(registration);
        }
    }
}

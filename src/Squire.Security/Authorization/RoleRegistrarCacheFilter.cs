namespace Squire.Security.Authorization
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Squire.Validation;

    public class RoleRegistrarCacheFilter : IRoleRegistrar
    {
        private readonly Dictionary<IRole, bool> cache;

        private readonly IRoleRegistrar registrar;

        public RoleRegistrarCacheFilter(IRoleRegistrar registrar)
        {
            registrar.VerifyParam("registrar").IsNotNull();
            this.registrar = registrar;
            this.cache = new Dictionary<IRole, bool>();
        }

        private void CacheRegistered(IRole role, bool isRegistered)
        {
            role.VerifyParam("role").IsNotNull();
            if (this.cache.ContainsKey(role))
            {
                this.cache[role] = isRegistered;
            }
            else
            {
                this.cache.Add(role, isRegistered);
            }
        }

        private bool? FromCache(IRole role)
        {
            role.VerifyParam("role").IsNotNull();
            if (!this.cache.ContainsKey(role))
            {
                return null;
            }

            return this.cache[role];
        }

        public void Register(IRole role)
        {
            role.VerifyParam("role").IsNotNull();
            this.registrar.Register(role);
            this.CacheRegistered(role, true);
        }

        public void Unregister(IRole role)
        {
            role.VerifyParam("role").IsNotNull();
            this.registrar.Unregister(role);
            this.CacheRegistered(role, false);
        }

        public bool IsRegistered(IRole role)
        {
            role.VerifyParam("role").IsNotNull();
            var cached = this.FromCache(role);
            if (cached.HasValue)
            {
                return cached.Value;
            }

            cached = this.registrar.IsRegistered(role);
            this.CacheRegistered(role, cached.Value);
            return cached.Value;
        }
    }
}

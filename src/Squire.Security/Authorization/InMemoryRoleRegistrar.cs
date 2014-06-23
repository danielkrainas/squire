namespace Squire.Security.Authorization
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Squire.Validation;

    public class InMemoryRoleRegistrar : IRoleRegistrar
    {
        private readonly List<IRole> roles;

        public InMemoryRoleRegistrar()
        {
            this.roles = new List<IRole>();
        }

        public void Register(IRole role)
        {
            role.VerifyParam("role").IsNotNull();
            if (!this.IsRegistered(role))
            {
                this.roles.Add(role);
            }
        }

        public void Unregister(IRole role)
        {
            role.VerifyParam("role").IsNotNull();
            this.roles.RemoveAll(r => r.Equals(role));
        }

        public bool IsRegistered(IRole role)
        {
            role.VerifyParam("role").IsNotNull();
            return this.roles.Any(r => r.Equals(role));
        }
    }
}

namespace Squire.Security.Authorization
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public interface IRoleRegistrar
    {
        void Register(IRole role);

        void Unregister(IRole role);

        bool IsRegistered(IRole role);
    }
}

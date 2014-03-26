namespace Squire.Sentinel.Authentication
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public interface IPlayerResolver
    {
        IPlayer Resolve(string name);

        IPlayer Resolve(RegistrationDetails registration);
    }
}

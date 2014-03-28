namespace Squire.Security.Authentication
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public interface IRoleResolver
    {
        ICollection<string> ResolveFor(IPlayer player);
    }
}

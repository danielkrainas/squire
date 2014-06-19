namespace Squire.Security.Authorization
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public interface IRole
    {
        string Id { get; }

        string Name { get; }
    }
}

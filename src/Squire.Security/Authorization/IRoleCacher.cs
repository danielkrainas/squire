namespace Squire.Security.Authorization
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public interface IRoleCacher
    {
        void InvalidateAll();

        void Invalidate(IRole role);

        IRole Retrieve(string id);

        IEnumerable<IRole> RetrieveAll();

        void Push(string id, IRole role);

        void PushAll(IEnumerable<IRole> all);

        bool SupportsAll { get; }

        bool HasAllCached { get; }
    }
}

namespace Squire.Storage
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IBlobResolver
    {
        IBlobContainer GetRoot();

        IBlobContainer ResolveContainer(Uri uri);

        IBlob Resolve(Uri uri);
    }
}

namespace Squire.Storage
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IBlobItem
    {
        long Size
        {
            get;
        }

        string Name
        {
            get;
        }

        BlobItemType Type
        {
            get;
        }

        Uri GetUri();
    }
}

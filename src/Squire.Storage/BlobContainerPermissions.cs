namespace Squire.Storage
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public struct BlobContainerPermissions
    {
        public BlobContainerAccessType PublicAccess
        {
            get;
            set;
        }
    }
}

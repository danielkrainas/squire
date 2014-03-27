namespace Squire.Storage
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IBlobContainer : IBlobItem
    {
        IBlob GetBlob(string name);

        void CreateIfNotExists();

        void SetPermissions(BlobContainerPermissions permissions);

        IBlobContainer GetContainer(string containerName);

        void Delete();

        IEnumerable<IBlob> SearchBlobs(string filter, bool recursive = false);

        IEnumerable<IBlobContainer> SearchContainers(string filter, bool recursive = false);

        IEnumerable<IBlobItem> Search(string filter, bool recursive = false);

        IEnumerable<IBlobItem> Contents
        {
            get;
        }
    }
}

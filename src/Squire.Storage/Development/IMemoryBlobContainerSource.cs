namespace Squire.Storage.Development
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public interface IMemoryBlobContainerSource
    {
        void AddContainer(MemoryBlobContainer container);

        void DeleteContainer(MemoryBlobContainer container);

        Uri GetUri();
    }
}

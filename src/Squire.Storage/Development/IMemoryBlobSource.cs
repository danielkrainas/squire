namespace Squire.Storage.Development
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public interface IMemoryBlobSource
    {
        Uri GetUri();

        void Remove(MemoryBlob memoryBlob);

        void Add(MemoryBlob memoryBlob);
    }
}

namespace Squire.Storage
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IBlob : IBlobItem
    {
        void SetPermissions(BlobPermissions permissions);

        void Delete();

        void PerformRead(Action<Stream> readerActivity);

        void PerformWrite(Action<Stream> writerActivity);

        void CopyTo(IBlobContainer container, string copyName = "");
    }
}

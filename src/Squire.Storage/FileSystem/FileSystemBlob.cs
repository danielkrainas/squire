namespace Squire.Storage.FileSystem
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Squire.Validation;

    public class FileSystemBlob : IBlob
    {
        public FileSystemBlob(FileInfo fileInfo)
        {
            this.FileInfo = fileInfo;
        }

        public FileInfo FileInfo
        {
            get;
            private set;
        }

        public long Size
        {
            get
            {
                if (this.FileInfo.Exists)
                {
                    return this.FileInfo.Length;
                }

                return 0;
            }
        }

        public void SetPermissions(BlobPermissions permissions)
        {
            
        }

        public void PerformRead(Action<Stream> streamActivity)
        {
            if (this.FileInfo.Exists)
            {
                using (var fs = File.Open(this.FileInfo.FullName, FileMode.OpenOrCreate, FileAccess.Read))
                {
                    streamActivity(fs);
                }
            }
        }

        public void PerformWrite(Action<Stream> streamActivity)
        {
            using (var fs = File.Open(this.FileInfo.FullName, FileMode.OpenOrCreate, FileAccess.Write))
            {
                streamActivity(fs);
            }
        }

        public void Delete()
        {
            if(this.FileInfo.Exists)
            {
                this.FileInfo.Delete();
            }
        }


        public string Name
        {
            get
            {
                return this.FileInfo.Name;
            }
        }

        public Uri GetUri()
        {
            return new Uri(this.FileInfo.FullName);
        }

        public void CopyTo(IBlobContainer container, string copyName = null)
        {
            this.CopyTo(container as FileSystemBlobContainer, copyName);
        }

        public void CopyTo(FileSystemBlobContainer container, string copyName = null)
        {
            container.VerifyParam("container").IsNotNull();
            this.FileInfo.CopyTo(Path.Combine(container.Directory.FullName, copyName ?? this.Name), true);
        }

        public BlobItemType Type
        {
            get
            {
                return BlobItemType.Blob;
            }
        }
    }
}

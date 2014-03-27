namespace Squire.Storage.Development
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using Squire.Validation;

    public class MemoryBlob : IBlob
    {
        private BlobPermissions permissions;

        private byte[] data;

        private bool exists;

        private MemoryBlob()
        {
            this.Name = string.Empty;
            this.permissions = new BlobPermissions();
            this.data = new byte[0];
            this.exists = false;
        }

        public MemoryBlob(IMemoryBlobSource source)
            : this()
        {
            this.Source = source;
        }

        public MemoryBlob(IMemoryBlobSource source, string name, bool exists)
            : this(source)
        {
            this.Name = name;
            this.exists = exists;
        }

        public void SetPermissions(BlobPermissions permissions)
        {
            this.permissions = permissions;
        }

        public void PerformRead(Action<Stream> streamActivity)
        {
            using (var ms = new MemoryStream(this.data))
            {
                streamActivity(ms);
            }
        }

        public void PerformWrite(Action<Stream> streamActivity)
        {
            using (var ms = new MemoryStream())
            {
                streamActivity(ms);
                this.data = ms.ToArray();
                if (!this.exists)
                {
                    this.Source.Add(this);
                    this.exists = true;
                }
            }
        }

        public void Delete()
        {
            this.Source.Remove(this);
            this.exists = false;
            this.data = new byte[0];
        }

        public long Size
        {
            get
            {
                return this.data.LongLength;
            }
        }

        public string Name
        {
            get;
            set;
        }

        public IMemoryBlobSource Source
        {
            get;
            set;
        }

        public BlobItemType Type
        {
            get
            {
                return BlobItemType.Blob;
            }
        }

        public Uri GetUri()
        {
            return new Uri(this.Source.GetUri(), this.Name);
        }

        public void CopyTo(IBlobContainer container, string copyName = null)
        {
            this.CopyTo(container as IMemoryBlobSource, copyName);
        }

        public void CopyTo(IMemoryBlobSource container, string copyName = null)
        {
            container.VerifyParam("container").IsNotNull();
            if (container != null)
            {
                var copyData = new byte[this.data.Length];
                Buffer.BlockCopy(this.data, 0, copyData, 0, this.data.Length);
                var copy = new MemoryBlob(container, copyName ?? this.Name, true);
                container.Add(copy);
                copy.data = copyData;
            }
        }
    }
}

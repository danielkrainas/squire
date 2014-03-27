namespace Squire.Storage.Development
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Squire.Validation;

    public class MemoryBlobContainer : IBlobContainer, IMemoryBlobContainerSource, IMemoryBlobSource
    {
        private bool exists;

        private readonly List<MemoryBlob> blobs;

        private readonly List<MemoryBlobContainer> containers;

        private BlobContainerPermissions permissions;

        public MemoryBlobContainer(IMemoryBlobContainerSource source, string name, bool exists)
            : this(source)
        {
            this.exists = exists;
            this.Name = name;
        }

        public MemoryBlobContainer(IMemoryBlobContainerSource source)
            : this()
        {
            this.Source = source;
        }

        private MemoryBlobContainer()
        {
            this.blobs = new List<MemoryBlob>();
            this.containers = new List<MemoryBlobContainer>();
            this.permissions = new BlobContainerPermissions();
        }

        public IBlob GetBlob(string name)
        {
            name.VerifyParam("name")
                .IsNotNull()
                .And.IsNotEmpty()
                .And.IsNotWhiteSpace();

            var blob = this.blobs.FirstOrDefault(b => b.Name == name);
            if (blob == null && this.exists)
            {
                return new MemoryBlob(this, name, false);
            }

            return blob;
        }

        public void CreateIfNotExists()
        {
            if (!this.exists)
            {
                this.Source.AddContainer(this);
                this.exists = true;
            }
        }

        public void SetPermissions(BlobContainerPermissions permissions)
        {
            this.permissions = permissions;
        }

        public IBlobContainer GetContainer(string containerName)
        {
            var container = this.containers.FirstOrDefault(c => c.Name == containerName);
            if (container == null)
            {
                container = new MemoryBlobContainer(this, containerName, false);
            }

            return container;
        }

        public IEnumerable<IBlobContainer> SearchContainers(string filter, bool recursive = false)
        {
            foreach (var c in this.containers)
            {
                if (c.Name.Contains(filter))
                {
                    yield return c;
                }

                if (recursive)
                {
                    foreach (var sub in c.SearchContainers(filter, true))
                    {
                        yield return sub;
                    }
                }
            }
        }

        public IEnumerable<IBlob> SearchBlobs(string filter, bool recursive = false)
        {
            foreach (var b in this.blobs)
            {
                if (b.Name.Contains(filter))
                {
                    yield return b;
                }
            }

            if (recursive)
            {
                foreach (var c in this.containers)
                {
                    foreach (var b in c.SearchBlobs(filter, true))
                    {
                        yield return b;
                    }
                }
            }
        }

        public IEnumerable<IBlobItem> Search(string filter, bool recursive = false)
        {
            foreach (var c in this.SearchContainers(filter, recursive))
            {
                yield return c;
            }

            foreach (var b in this.SearchBlobs(filter, recursive))
            {
                yield return b;
            }
        }

        public void Delete()
        {
            if (this.exists)
            {
                this.Source.DeleteContainer(this);
                this.exists = false;
            }
        }

        public IEnumerable<IBlobItem> Contents
        {
            get
            {
                foreach (var c in this.containers)
                {
                    yield return c;
                }

                foreach (var b in this.blobs)
                {
                    yield return b;
                }
            }
        }

        public long Size
        {
            get
            {
                return 0L;
            }
        }

        public string Name
        {
            get;
            set;
        }

        public BlobItemType Type
        {
            get
            {
                return BlobItemType.Container;
            }
        }

        public IMemoryBlobContainerSource Source
        {
            get;
            set;
        }

        public Uri GetUri()
        {
            return new Uri(this.Source.GetUri(), this.Name);
        }

        public void AddContainer(MemoryBlobContainer container)
        {
            this.containers.Add(container);
        }

        public void DeleteContainer(MemoryBlobContainer container)
        {
            this.containers.Remove(container);
        }

        void IMemoryBlobSource.Remove(MemoryBlob blob)
        {
            this.blobs.Remove(blob);
        }

        void IMemoryBlobSource.Add(MemoryBlob blob)
        {
            this.blobs.Add(blob);
            blob.Source = this;
        }
    }
}

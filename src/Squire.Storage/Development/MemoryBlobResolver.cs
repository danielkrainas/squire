namespace Squire.Storage.Development
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Squire.Validation;

    public class MemoryBlobResolver : IBlobResolver, IMemoryBlobContainerSource
    {
        private readonly MemoryBlobContainer root;

        private readonly List<MemoryBlobContainer> containers;

        private readonly bool autoCreateUnknown;

        public MemoryBlobResolver(bool autoCreateUnknown = true)
        {
            this.root = new MemoryBlobContainer(this, "$", true, autoCreateUnknown);
            this.containers = new List<MemoryBlobContainer>();
            this.containers.Add(this.root);
            this.autoCreateUnknown = autoCreateUnknown;
        }

        public virtual bool AutoCreateUnknown
        {
            get
            {
                return this.autoCreateUnknown;
            }
        }

        public void AddContainer(MemoryBlobContainer container)
        {
            this.containers.Add(container);
        }

        public void DeleteContainer(MemoryBlobContainer container)
        {
            this.containers.Remove(container);
        }

        public Uri GetUri()
        {
            return new Uri("http://memory");
        }

        public IBlobContainer GetRoot()
        {
            return this.root;
        }

        public IBlobContainer ResolveContainer(Uri uri)
        {
            if (!uri.IsAbsoluteUri)
            {
                uri = new Uri(this.GetUri(), uri.ToString());
            }

            var path = uri.GetComponents(UriComponents.Path, UriFormat.SafeUnescaped);
            var pathParts = path.Split('/');
            var collection = this.GetRoot();
            foreach (var part in pathParts)
            {
                collection = collection.GetContainer(part);
                if (collection == null)
                {
                    break;
                }
            }

            return collection;
        }

        public IBlob Resolve(Uri uri)
        {
            if (!uri.IsAbsoluteUri)
            {
                uri = new Uri(this.GetUri(), uri.ToString());
            }

            var path = uri.GetComponents(UriComponents.Path, UriFormat.SafeUnescaped);
            var pathParts = path.Split('/');
            var collection = this.GetRoot();
            IBlob result = null;
            var i = 0;
            foreach (var part in pathParts)
            {
                if (++i == pathParts.Length)
                {
                    result = collection.GetBlob(part);
                }
                else
                {
                    collection = collection.GetContainer(part);
                }

                if (collection == null)
                {
                    break;
                }
            }

            return result;
        }
    }
}

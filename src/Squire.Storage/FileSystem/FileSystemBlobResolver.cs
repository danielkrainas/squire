namespace Squire.Storage.FileSystem
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Squire.Validation;

    public class FileSystemBlobResolver : IBlobResolver
    {
        private FileSystemBlobContainer root = null;

        private string rootDirectory = null;

        public FileSystemBlobResolver(string rootDirectory, bool allowExternal = false)
        {
            this.AllowExternal = allowExternal;
            rootDirectory.VerifyParam("rootDirectory")
                .IsNotNull()
                .And.IsNotEmpty();

            this.RootDirectory = rootDirectory;
        }

        #region Properties

        public bool AllowExternal
        {
            get;
            private set;
        }

        public string RootDirectory
        {
            get
            {
                return this.root.Directory.FullName;
            }
            set
            {
                this.rootDirectory = value;
                this.root = new FileSystemBlobContainer(new DirectoryInfo(this.rootDirectory));
            }
        }

        #endregion // Properties

        public IBlobContainer GetRoot()
        {
            return this.root;
        }

        public IBlobContainer ResolveContainer(Uri uri)
        {
            if (uri.Scheme != Uri.UriSchemeFile)
            {
                throw new InvalidOperationException(string.Format("uri schema '{0}' is not supported by this resolver", uri.Scheme));
            }

            if (this.AllowExternal)
            {
                return new FileSystemBlobContainer(new DirectoryInfo(uri.LocalPath));
            }

            if (this.root.GetUri().IsBaseOf(uri))
            {
                return new FileSystemBlobContainer(new DirectoryInfo(uri.LocalPath));
            }

            return null;
        }

        public IBlob Resolve(Uri uri)
        {
            if (uri.Scheme != Uri.UriSchemeFile)
            {
                throw new InvalidOperationException(string.Format("uri schema '{0}' is not supported by this resolver", uri.Scheme));
            }

            if (this.AllowExternal)
            {
                return new FileSystemBlob(new FileInfo(uri.LocalPath));
            }

            if (this.root.GetUri().IsBaseOf(uri))
            {
                return new FileSystemBlob(new FileInfo(uri.LocalPath));
            }

            return null;
        }
    }
}

namespace Squire.Storage.FileSystem
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class FileSystemBlobContainer : IBlobContainer
    {
        public FileSystemBlobContainer(string directoryPath)
        {
            this.Directory = new DirectoryInfo(directoryPath);
        }

        public FileSystemBlobContainer(DirectoryInfo directory)
        {
            this.Directory = directory;
        }

        public DirectoryInfo Directory
        {
            get;
            private set;
        }

        public IBlob GetBlob(string name)
        {
            var fileInfo = this.Directory.EnumerateFiles(name, SearchOption.TopDirectoryOnly).FirstOrDefault();
            if (fileInfo == null)
            {
                fileInfo = new FileInfo(Path.Combine(this.Directory.FullName, name));
            }

            return new FileSystemBlob(fileInfo);
        }

        public void CreateIfNotExists()
        {
            if (!this.Directory.Exists)
            {
                this.Directory.Create();
            }
        }

        public void SetPermissions(BlobContainerPermissions permissions)
        {
            throw new NotImplementedException();
        }

        public IBlobContainer GetContainer(string containerName)
        {
            var containerInfo = this.Directory.EnumerateDirectories(containerName, SearchOption.TopDirectoryOnly).FirstOrDefault();
            if (containerInfo == null)
            {
                return null;
            }

            return new FileSystemBlobContainer(containerInfo);
        }

        public void Delete()
        {
            if (this.Directory.Exists)
            {
                this.Directory.Delete();
            }
        }

        public long Size
        {
            get 
            {
                if (this.Directory.Exists)
                {
                    return this.Directory.EnumerateFiles().Sum(f => f.Length);
                }

                return 0;
            }
        }

        public string Name
        {
            get
            {
                return this.Directory.Name;
            }
        }

        public BlobItemType Type
        {
            get
            {
                return BlobItemType.Container;
            }
        }

        public Uri GetUri()
        {
            return new Uri(this.Directory.FullName);
        }

        public IEnumerable<IBlobItem> Contents
        {
            get
            {
                foreach (var entry in this.Directory.EnumerateDirectories())
                {
                    if (entry.Name != ".." && entry.Name != ".")
                    {
                        yield return new FileSystemBlobContainer(entry);
                    }
                }

                foreach (var entry in this.Directory.EnumerateFiles())
                {
                    yield return new FileSystemBlob(entry);
                }
            }
        }


        public IEnumerable<IBlob> SearchBlobs(string filter, bool recursive = false)
        {
            foreach (var entry in this.Directory.EnumerateFiles(filter, recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly))
            {
                yield return new FileSystemBlob(entry);
            }
        }

        public IEnumerable<IBlobContainer> SearchContainers(string filter, bool recursive = false)
        {
            foreach (var entry in this.Directory.EnumerateDirectories(filter, recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly))
            {
                if (entry.Name != ".." && entry.Name != ".")
                {
                    yield return new FileSystemBlobContainer(entry);
                }
            }
        }

        public IEnumerable<IBlobItem> Search(string filter, bool recursive = false)
        {
            foreach (var container in this.SearchContainers(filter, recursive))
            {
                yield return container;
            }

            foreach (var blob in this.SearchBlobs(filter, recursive))
            {
                yield return blob;
            }
        }
    }
}

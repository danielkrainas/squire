namespace Squire.Storage
{
    using Squire.Setup;
    using Squire.Storage.Development;
    using Squire.Storage.FileSystem;
    using Squire.Validation;

    public static class AppSetupStorageExtensions
    {
        public static IAdvancedAppSetup<T, MemoryBlobResolver> ConfigureInMemoryBlob<T>(this IAppSetup<T> setup, bool autoCreateUnknown = true)
        {
            return new AdvancedAppSetup<T, MemoryBlobResolver>(setup, new MemoryBlobResolver(autoCreateUnknown));
        }

        public static IAdvancedAppSetup<T, FileSystemBlobResolver> ConfigureFileSystemBlobs<T>(this IAppSetup<T> setup, string rootDirectory, bool allowExternal)
        {
            return new AdvancedAppSetup<T, FileSystemBlobResolver>(setup, new FileSystemBlobResolver(rootDirectory, allowExternal));
        }

        public static IAdvancedAppSetup<T, MemoryBlobResolver> AddBlob<T>(this IAdvancedAppSetup<T, MemoryBlobResolver> setup, string path, string contents = "")
        {
            path.VerifyParam("path").IsNotNull();
            contents.VerifyParam("contents").IsNotNull();
            var pathParts = path.Split('/');
            var container = ((MemoryBlobContainer)setup.Advanced.GetRoot()).GetOrCreateContainer(pathParts[0]);
            for (var i = 1; i < (pathParts.Length - 1); i++)
            {
                container = container.GetOrCreateContainer(pathParts[i]);
            }

            var blob = container.GetOrCreateBlob(pathParts[pathParts.Length - 1]);
            blob.WriteAllText(contents);
            return setup;
        }
    }
}

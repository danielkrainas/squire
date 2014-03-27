namespace Squire.Storage
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class StorageException : InvalidOperationException
    {
        public StorageException(Exception innerException)
            : this("an error ocurred when attempting a storage operation. See inner-exception for more details.", innerException)
        {
        }

        public StorageException(string message)
            : base(message)
        {
        }

        public StorageException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}

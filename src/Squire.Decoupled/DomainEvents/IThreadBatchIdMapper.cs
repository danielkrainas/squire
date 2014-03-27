namespace Squire.Decoupled.DomainEvents
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IThreadBatchIdMapper
    {
        bool IsActive
        {
            get;
        }

        Guid GetBatchId();

        Guid Create(object unitOfWork);

        Guid Release(object unitOfWork);
    }
}

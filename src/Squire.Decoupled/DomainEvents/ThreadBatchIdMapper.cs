namespace Squire.Decoupled.DomainEvents
{
    using Squire.Validation;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;

    public class ThreadBatchIdMapper : IThreadBatchIdMapper
    {
        private class UnitOfWorkGuidMapping
        {
            public UnitOfWorkGuidMapping(object unitOfWork, Guid guid)
            {
                this.UnitOfWork = unitOfWork;
                this.Guid = guid;
            }

            public Guid Guid
            {
                get;
                private set;
            }

            public object UnitOfWork
            {
                get;
                private set;
            }
        }

        private readonly Dictionary<int, ICollection<UnitOfWorkGuidMapping>> threadMappings;

        public ThreadBatchIdMapper()
        {
            this.threadMappings = new Dictionary<int, ICollection<UnitOfWorkGuidMapping>>();
        }

        public bool IsActive
        {
            get
            {
                ICollection<UnitOfWorkGuidMapping> mappings;
                return this.threadMappings.TryGetValue(Thread.CurrentThread.ManagedThreadId, out mappings) && mappings.Any();
            }
        }

        public Guid GetBatchId()
        {
            ICollection<UnitOfWorkGuidMapping> mappings;
            if (!this.threadMappings.TryGetValue(Thread.CurrentThread.ManagedThreadId, out mappings))
            {
                return Guid.Empty;
            }

            var lastMapping = mappings.LastOrDefault();
            return lastMapping == null ? Guid.Empty : lastMapping.Guid;
        }

        public Guid Create(object unitOfWork)
        {
            unitOfWork.VerifyParam("unitOfWork").IsNotNull();
            ICollection<UnitOfWorkGuidMapping> mappings;
            if (!this.threadMappings.TryGetValue(Thread.CurrentThread.ManagedThreadId, out mappings))
            {
                mappings = new List<UnitOfWorkGuidMapping>();
                threadMappings[Thread.CurrentThread.ManagedThreadId] = mappings;
            }

            var guid = Guid.NewGuid();
            mappings.Add(new UnitOfWorkGuidMapping(unitOfWork, guid));
            return guid;
        }

        public Guid Release(object unitOfWork)
        {
            unitOfWork.VerifyParam("unitOfWork").IsNotNull();
            ICollection<UnitOfWorkGuidMapping> mappings;
            if (!this.threadMappings.TryGetValue(Thread.CurrentThread.ManagedThreadId, out mappings))
            {
                throw new InvalidOperationException(string.Format("Unit of work '{0}' has not been registered", unitOfWork));
            }

            var batchId = Guid.Empty;
            foreach (var mapping in mappings.Where(m => m.UnitOfWork == unitOfWork))
            {
                batchId = mapping.Guid;
                mappings.Remove(mapping);
                break;
            }

            if (batchId == Guid.Empty)
            {
                throw new InvalidOperationException(string.Format("Unit of work '{0}' has not been registered", unitOfWork));
            }

            return batchId;
        }
    }
}

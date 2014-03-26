namespace Squire.Unhinged.DomainEvents.Pipeline
{
    using Squire.Validation;
    using Squire.Unhinged.DomainEvents.Pipeline.Messages;
    using Squire.Unhinged.Pipeline;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class TransactionalDomainEventHandler : IDownstreamHandler, IUnitOfWorkObserver
    {
        private readonly IDomainEventStorage storage;
        private readonly IThreadBatchIdMapper threadBatchIdMapper;
        private IDownstreamContext context;

        public TransactionalDomainEventHandler(IUnitOfWorkAdapter adapter, IDomainEventStorage storage)
        {
            ValidationHelper.ArgumentNotNull(adapter, "adapter");
            ValidationHelper.ArgumentNotNull(storage, "storage");
            this.storage = storage;
            adapter.Register(this);
            this.threadBatchIdMapper = new ThreadBatchIdMapper();
        }

        public TransactionalDomainEventHandler(IUnitOfWorkAdapter adapter, IDomainEventStorage storage, IThreadBatchIdMapper threadBatchIdMapper)
        {
            ValidationHelper.ArgumentNotNull(adapter, "adapter");
            ValidationHelper.ArgumentNotNull(storage, "storage");
            ValidationHelper.ArgumentNotNull(threadBatchIdMapper, "threadBatchIdMapper");
            this.storage = storage;
            adapter.Register(this);
            this.threadBatchIdMapper = threadBatchIdMapper;
        }

        public void Create(object unitOfWork)
        {
            this.threadBatchIdMapper.Create(unitOfWork);
        }

        public void Release(object unitOfWork, bool successful)
        {
            var guid = this.threadBatchIdMapper.Release(unitOfWork);
            if (successful)
            {
                var jobs = this.storage.Release(guid);
                foreach (var job in jobs)
                {
                    this.context.SendDownstream(new DispatchEvent(job));
                }
            }
            else
            {
                this.storage.Delete(guid);
            }
        }

        public void HandleDownstream(IDownstreamContext context, IDownstreamMessage message)
        {
            this.context = context;
            var dispatch = message as DispatchEvent;
            if (dispatch != null)
            {
                var batchId = this.threadBatchIdMapper.GetBatchId();
                if (batchId != Guid.Empty)
                {
                    this.storage.Hold(batchId, dispatch.DomainEvent);
                    return;
                }
            }

            context.SendDownstream(message);
        }
    }
}

namespace Squire.Decoupled.DomainEvents
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IUnitOfWorkObserver
    {
        void Create(object unitOfWork);

        void Release(object unitOfWork, bool successful);
    }
}

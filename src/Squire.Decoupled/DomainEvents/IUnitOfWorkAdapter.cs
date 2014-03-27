﻿namespace Squire.Decoupled.DomainEvents
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IUnitOfWorkAdapter
    {
        void Register(IUnitOfWorkObserver observer);
    }
}

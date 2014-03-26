﻿namespace Squire.Unhinged.Aspects
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IAdviceProvider
    {
        IEnumerable<Advice> GetAdvices(JoinPointDescriptor joinPoint);
    }
}

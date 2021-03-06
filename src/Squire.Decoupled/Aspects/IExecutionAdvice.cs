﻿namespace Squire.Decoupled.Aspects
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IExecutionAdvice : IAdvice
    {
        void BeforeExecute(BeforeExecutionContext adviceContext);

        void AfterExecute(AfterExecutionContext adviceContext);
    }
}

namespace Squire.Validation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class ValidationAnd<T>
    {
        public ValidationAnd(T target)
        {
            this.And = target;
        }

        public T And
        {
            get;
            private set;
        }
    }
}

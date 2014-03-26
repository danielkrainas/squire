namespace Squire.Validation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class FluentArgument<T>
    {
        public FluentArgument(T target, string name)
        {
            this.Target = target;
            this.Name = name;
        }

        public string Name
        {
            get;
            private set;
        }

        public T Target
        {
            get;
            private set;
        }
    }
}

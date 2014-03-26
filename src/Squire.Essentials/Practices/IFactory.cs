namespace Squire.Practices
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public interface IFactory<T>
    {
        T Create();
    }
}

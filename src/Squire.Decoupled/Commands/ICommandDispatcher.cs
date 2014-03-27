namespace Squire.Decoupled.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface ICommandDispatcher
    {
        void Dispatch<T>(T command) where T : class, ICommand;
    }
}

namespace Squire.Unhinged.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IHandleCommand<in TCommand> 
        where TCommand : class, ICommand
    {
        void Invoke(TCommand command);
    }
}

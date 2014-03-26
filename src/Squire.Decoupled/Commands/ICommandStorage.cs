namespace Squire.Unhinged.Commands
{
    using Squire.Unhinged.Commands.Pipeline.Messages;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface ICommandStorage
    {
        void Add(DispatchCommand command);

        void Delete(ICommand command);

        void Update(DispatchCommand command);

        IEnumerable<DispatchCommand> FindFailedCommands(DateTime processedBefore);
    }
}

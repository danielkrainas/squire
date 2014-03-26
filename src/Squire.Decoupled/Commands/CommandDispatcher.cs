namespace Squire.Unhinged.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public static class CommandDispatcher
    {
        private static ICommandDispatcher _dispatcher;

        public static void Assign(ICommandDispatcher dispatcher)
        {
            CommandDispatcher._dispatcher = dispatcher;
        }

        public static void Dispatch<TCommand>(TCommand command)
            where TCommand : class, ICommand
        {
            if (CommandDispatcher._dispatcher == null)
            {
                throw new InvalidOperationException("the default dispatcher has not been specified");
            }

            CommandDispatcher._dispatcher.Dispatch(command);
        }
    }
}

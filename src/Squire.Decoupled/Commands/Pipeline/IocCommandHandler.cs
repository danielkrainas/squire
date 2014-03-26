namespace Squire.Unhinged.Commands.Pipeline
{
    using Squire.Validation;
    using Squire.Unhinged.Commands.Pipeline.Messages;
    using Squire.Unhinged.Pipeline;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.Practices.ServiceLocation;

    public class IocCommandHandler : IDownstreamHandler
    {
        private static readonly MethodInfo _method;

        static IocCommandHandler()
        {
            IocCommandHandler._method = typeof(IocCommandHandler).GetMethod("Dispatch", BindingFlags.Instance | BindingFlags.NonPublic);
        }

        private readonly IServiceLocator locator;

        public IocCommandHandler(IServiceLocator locator)
        {
            locator.VerifyParam("locator").IsNotNull();
            this.locator = locator;
        }

        public void HandleDownstream(IDownstreamContext context, IDownstreamMessage message)
        {
            var dispatchCommand = message as DispatchCommand;
            if (dispatchCommand != null)
            {
                try
                {
                    IocCommandHandler._method.MakeGenericMethod(dispatchCommand.Command.GetType()).Invoke(this, new object[] { dispatchCommand.Command });
                    context.SendUpstream(new CommandCompleted(dispatchCommand));
                }
                catch (Exception e)
                {
                    dispatchCommand.AddFailedAttempt();
                    context.SendUpstream(new CommandFailed(dispatchCommand, e));
                }
            }
            else
            {
                context.SendDownstream(message);
            }
        }

        private void Dispatch<TCommand>(TCommand command) 
            where TCommand : class, ICommand
        {
            this.locator.GetInstance<IHandleCommand<TCommand>>().Invoke(command);
        }
    }
}

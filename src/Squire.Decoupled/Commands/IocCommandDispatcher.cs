namespace Squire.Decoupled.Commands
{
    using Microsoft.Practices.ServiceLocation;
    using Squire.Validation;

    public class IocCommandDispatcher : ICommandDispatcher
    {
        private readonly IServiceLocator locator;

        public IocCommandDispatcher(IServiceLocator locator)
        {
            locator.VerifyParam("locator").IsNotNull();
            this.locator = locator;
        }

        public void Dispatch<TCommand>(TCommand command) 
            where TCommand : class, ICommand
        {
            command.VerifyParam("command").IsNotNull();
            this.locator.GetInstance<IHandleCommand<TCommand>>().Invoke(command);
        }
    }
}

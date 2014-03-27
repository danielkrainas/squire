namespace Squire.Decoupled.Commands
{
    using Squire.Validation;
    using Squire.Decoupled.Commands.Pipeline.Messages;
    using Squire.Decoupled.Pipeline;
    using Squire.Decoupled.Pipeline.Messages;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class PipelineDispatcher : ICommandDispatcher, IDisposable
    {
        private readonly IPipeline pipeline;

        public PipelineDispatcher(IPipeline pipeline)
        {
            ValidationHelper.ArgumentNotNull(pipeline, "pipeline");
            this.pipeline = pipeline;
        }

        public void Dispatch<TCommand>(TCommand command)
            where TCommand : class, ICommand
        {
            ValidationHelper.ArgumentNotNull(command, "command");
            var message = new DispatchCommand(command);
            this.pipeline.Send(message);
        }

        public void Dispose()
        {
            this.pipeline.Send(new Shutdown());
        }
    }
}

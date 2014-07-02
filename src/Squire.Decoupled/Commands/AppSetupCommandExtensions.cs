namespace Squire.Decoupled.Commands
{
    using Squire.Decoupled.Pipeline;
    using Squire.Setup;
    using Squire.Validation;
    using System;
    using System.Linq.Expressions;

    public static class AppSetupCommandExtensions
    {
        public static IAppSetup<T> Commands<T>(this IAppSetup<T> setup, Expression<Func<PipelineDispatcherBuilder, PipelineDispatcherBuilder>> configure, IUpstreamHandler errorHandler = null)
        {
            configure.VerifyParam("configure").IsNotNull();
            errorHandler = errorHandler ?? new DevNullUpstreamHandler();
            var builder = configure.Compile()(new PipelineDispatcherBuilder(errorHandler));
            if (builder == null)
            {
                throw new InvalidOperationException("the result of the configuration cannot be null.");
            }

            CommandDispatcher.Assign(builder.Build());
            return setup;
        }
    }
}

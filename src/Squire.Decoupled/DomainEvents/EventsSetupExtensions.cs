namespace Squire.Decoupled.DomainEvents
{
    using Squire.Decoupled.Pipeline;
    using Squire.Setup;
    using Squire.Validation;
    using System;
    using System.Linq.Expressions;

    public static class EventsSetupExtensions
    {
        public static IAppSetup<T> Events<T>(this IAppSetup<T> setup, Expression<Func<EventPipelineBuilder, EventPipelineBuilder>> configure, IUpstreamHandler errorHandler = null)
        {
            configure.VerifyParam("configure").IsNotNull();
            errorHandler = errorHandler ?? new DevNullUpstreamHandler();
            var builder = configure.Compile()(new EventPipelineBuilder(errorHandler));
            if (builder == null)
            {
                throw new InvalidOperationException("the result of the configuration cannot be null.");
            }

            DomainEvent.Assign(builder.Build());
            return setup;
        }
    }
}

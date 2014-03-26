namespace Squire.Unhinged.Queries
{
    using Squire.Validation;
    using Microsoft.Practices.ServiceLocation;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;

    public class IocQueryDispatcher : IDispatchQuery
    {
        private static readonly MethodInfo _method;

        private readonly IServiceLocator container;

        static IocQueryDispatcher()
        {
            IocQueryDispatcher._method = typeof(IocQueryDispatcher).GetMethod("ExecuteInternal", BindingFlags.NonPublic | BindingFlags.Instance);
        }

        public IocQueryDispatcher(IServiceLocator container)
        {
            ValidationHelper.ArgumentNotNull(container, "container");
            this.container = container;
        }

        public TResult Execute<TResult>(IQuery<TResult> query)
        {
            var genericExecute = IocQueryDispatcher._method.MakeGenericMethod(typeof(TResult), query.GetType());
            return (TResult)genericExecute.Invoke(this, new object[] { query });
        }

        protected TResult ExecuteInternal<TResult, TQuery>(TQuery query)
            where TQuery : IQuery<TResult>
        {
            var handlerType = typeof(IExecuteQuery<TQuery, TResult>);
            var handler = (IExecuteQuery<TQuery, TResult>)this.container.GetService(handlerType);
            return handler.Execute(query);
        }
    }
}

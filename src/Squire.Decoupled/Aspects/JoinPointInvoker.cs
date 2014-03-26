namespace Squire.Unhinged.Aspects
{
    using Squire.Validation;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public static class JoinPointInvoker
    {
        private static IDictionary<string, object> GetParameterValues(JoinPointDescriptor joinPoint, object[] parameterValues)
        {
            var parameters = new Dictionary<string, object>();
            var i = 0;
            foreach(var parameter in joinPoint.GetParameters())
            {
                if (i < parameterValues.Length)
                {
                    parameters.Add(parameter.ParameterName, parameterValues[i]);
                }
                else
                {
                    parameters.Add(parameter.ParameterName, parameter.DefaultValue);
                }

                i++;
            }

            return parameters;
        }

        private static AfterExecutionContext InvokeAdvice(IExecutionAdvice advice, BeforeExecutionContext preContext, Func<AfterExecutionContext> continuation)
        {
            advice.BeforeExecute(preContext);
            if (preContext.HasResult && preContext.IsResultSet)
            {
                return new AfterExecutionContext(preContext.JoinPoint, true, null)
                {
                    Result = preContext.Result
                };
            }

            var wasError = false;
            AfterExecutionContext postContext = null;
            try
            {
                postContext = continuation();
            }
            catch (Exception ex)
            {
                wasError = true;
                postContext = new AfterExecutionContext(preContext.JoinPoint, false, ex);
                advice.AfterExecute(postContext);
                if (!postContext.ExceptionHandled)
                {
                    throw;
                }
            }

            if (!wasError)
            {
                advice.AfterExecute(postContext);
            }

            return postContext;
        }

        private static AfterExecutionContext InvokeAspect(JoinPointDescriptor joinPoint, IList<IExecutionAdvice> advices, IDictionary<string, object> parameters)
        {
            var preContext = new BeforeExecutionContext(joinPoint, parameters);
            Func<AfterExecutionContext> continuation = () => new AfterExecutionContext(joinPoint);
            Func<AfterExecutionContext> thunk = advices.Reverse().Aggregate(continuation, (next, advice) => () => JoinPointInvoker.InvokeAdvice(advice, preContext, next));
            return thunk();
        }

        private static AfterExecutionContext InvokeJoinPointInternal<TDelegate>(TDelegate joinPoint, params object[] parameterValues)
            where TDelegate : class
        {
            joinPoint.VerifyParam("joinPoint").IsNotNull()
                .And.IsSubClassOf(typeof(Delegate));
            var joinPointDelegate = joinPoint as Delegate;
            var joinPointDescriptor = new JoinPointDescriptor(joinPointDelegate.Method);
            var adviceInfo = new AdviceInfo(AdviceProviders.Providers.GetAdvice(joinPointDescriptor));
            try
            {
                var parameters = JoinPointInvoker.GetParameterValues(joinPointDescriptor, parameterValues);
                var postContext = JoinPointInvoker.InvokeAspect(joinPointDescriptor, adviceInfo.ExecutionAdvices, parameters);
                return postContext;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static TResult InvokeJoinPoint<TDelegate, TResult>(TDelegate joinPoint, params object[] parameterValues)
            where TDelegate : class
        {
            var postContext = JoinPointInvoker.InvokeJoinPointInternal(joinPoint, parameterValues);
            if (postContext != null && !postContext.JoinPoint.HasVoidResult)
            {
                return (TResult)postContext.Result;
            }

            return default(TResult);
        }

        public static void InvokeJoinPoint<TDelegate>(TDelegate joinPoint, params object[] parameterValues)
            where TDelegate : class
        {
            JoinPointInvoker.InvokeJoinPointInternal(joinPoint, parameterValues);
        }
    }
}

namespace Squire.Security.Authentication
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Squire.Validation;
    using System.Linq.Expressions;

    public class InlineHashFilter : IHashFilter
    {
        private readonly Func<string, string, string> hashFilter;

        public InlineHashFilter(Expression<Func<string, string, string>> hashFilterExpression)
        {
            hashFilterExpression.VerifyParam("hashFilterExpression").IsNotNull();
            this.hashFilter = hashFilterExpression.Compile();
        }

        public InlineHashFilter(Func<string, string, string> hashFilter)
        {
            hashFilter.VerifyParam("hashFilter").IsNotNull();
            this.hashFilter = hashFilter;
        }

        public string Calculate(string playerName, string hashTarget)
        {
            return this.hashFilter(playerName, hashTarget);
        }
    }
}

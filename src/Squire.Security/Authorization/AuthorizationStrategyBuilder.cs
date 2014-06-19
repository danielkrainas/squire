namespace Squire.Security.Authorization
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class AuthorizationStrategyBuilder
    {
        private IRoleResolver resolver;

        private IRoleCacher cacher;

        private IRoleRegistrar registrar;

        private IRoleTracker tracker;

        public AuthorizationStrategyBuilder()
        {
            this.resolver = null;
            this.cacher = null;
            this.registrar = null;
            this.tracker = null;
        }

        public AuthorizationStrategyBuilder ResolveWith(IRoleResolver resolver)
        {
            this.resolver = resolver;
            return this;
        }

        public AuthorizationStrategyBuilder AllowRegistration(IRoleRegistrar registrar)
        {
            this.registrar = registrar;
            return this;
        }

        public AuthorizationStrategyBuilder Caching(IRoleCacher cacher)
        {
            this.cacher = cacher;
            return this;
        }

        public AuthorizationStrategyBuilder TrackWith(IRoleTracker tracker)
        {
            this.tracker = tracker;
            return this;
        }

        public IAuthorizationStrategy Build()
        {
            var topResolver = this.resolver;
            if (this.resolver == null)
            {
                throw new InvalidOperationException("you must specify a role resolver");
            }

            if(this.tracker == null)
            {
                throw new InvalidOperationException("you must specify a role tracker");
            }

            if (this.cacher != null)
            {
                topResolver = new RoleCacheResolveFilter(this.cacher, this.resolver);
            }

            return new AuthorizationStrategyAdapter(topResolver, this.tracker, registrar: this.registrar);
        }
    }
}

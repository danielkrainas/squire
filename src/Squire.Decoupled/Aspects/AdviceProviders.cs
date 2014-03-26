namespace Squire.Unhinged.Aspects
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public static class AdviceProviders
    {
        static AdviceProviders()
        {
            AdviceProviders.Providers = new AdviceProviderCollection();
            AdviceProviders.Providers.Add(GlobalAdvice.Advice);
        }

        public static AdviceProviderCollection Providers
        {
            get;
            private set;
        }
    }
}

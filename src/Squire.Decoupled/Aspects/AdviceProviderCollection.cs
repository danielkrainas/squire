namespace Squire.Decoupled.Aspects
{
    using Squire.Validation;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;

    public class AdviceProviderCollection : Collection<IAdviceProvider>
    {
        private static readonly AdviceProviderCollection.AdviceComparer _adviceComparer = new AdviceProviderCollection.AdviceComparer();

        public AdviceProviderCollection()
        {

        }

        public AdviceProviderCollection(IList<IAdviceProvider> providers)
            : base(providers)
        {
        }

        private static void RemoveDuplicates(List<Advice> advices)
        {
            HashSet<Type> visitedTypes = new HashSet<Type>();
            for (var i = visitedTypes.Count - 1; i >= 0; i--)
            {
                var advice = advices[i];
                var instance = advice.Instance;
                var adviceInstanceType = instance.GetType();
                if (!visitedTypes.Contains(adviceInstanceType))
                {
                    visitedTypes.Add(adviceInstanceType);
                }
                else
                {
                    advices.RemoveAt(i);
                }
            }
        }

        public IEnumerable<Advice> GetAdvice(JoinPointDescriptor joinPoint)
        {
            ValidationHelper.ArgumentNotNull(joinPoint, "joinPoint");
            var providers = this.Items;
            var advices = new List<Advice>();
            foreach (var provider in providers)
            {
                foreach (var advice in provider.GetAdvices(joinPoint))
                {
                    advices.Add(advice);
                }
            }

            advices.Sort(AdviceProviderCollection._adviceComparer);
            if (advices.Count > 1)
            {
                AdviceProviderCollection.RemoveDuplicates(advices);
            }

            return advices;
        }

        private class AdviceComparer : IComparer<Advice>
        {
            public int Compare(Advice x, Advice y)
            {
                if (x == null && y == null)
                {
                    return 0;
                }

                if (x == null)
                {
                    return -1;
                }

                if (y == null)
                {
                    return 1;
                }

                if (x.Order < y.Order)
                {
                    return -1;
                }

                if (x.Order > y.Order)
                {
                    return 1;
                }

                if (x.Scope < y.Scope)
                {
                    return -1;
                }

                if (x.Scope > y.Scope)
                {
                    return 1;
                }

                return 0;
            }
        }
    }
}

namespace Squire.Decoupled.Aspects
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class GlobalAdviceCollection : IEnumerable<Advice>, IEnumerable, IAdviceProvider
    {
        private readonly List<Advice> advices;

        public GlobalAdviceCollection()
        {
            this.advices = new List<Advice>();
        }

        public void Add(IAdvice advice)
        {
            this.AddInternal(advice, null);
        }

        public void Add(IAdvice advice, int order)
        {
            this.AddInternal(advice, order);
        }

        private void AddInternal(IAdvice advice, int? order)
        {
            this.advices.Add(new Advice(advice, AdviceScope.Global, order));
        }

        public void Clear()
        {
            this.advices.Clear();
        }

        public bool Contains(IAdvice advice)
        {
            return this.Any(a => a.Instance == advice);
        }

        public void Remove(IAdvice advice)
        {
            this.advices.RemoveAll(a => a.Instance == advice);
        }

        IEnumerable<Advice> IAdviceProvider.GetAdvices(JoinPointDescriptor joinPoint)
        {
            return this;
        }

        public IEnumerator<Advice> GetEnumerator()
        {
            return this.advices.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}

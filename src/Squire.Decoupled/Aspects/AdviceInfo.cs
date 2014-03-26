namespace Squire.Unhinged.Aspects
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class AdviceInfo
    {
        private readonly List<IExecutionAdvice> executionAdvices;

        public AdviceInfo()
        {
            this.executionAdvices = new List<IExecutionAdvice>();
        }

        public AdviceInfo(IEnumerable<Advice> advices)
            : this()
        {
            this.SplitAdvices(advices);
        }

        private void SplitAdvices(IEnumerable<Advice> advices)
        {
            foreach (var advice in advices)
            {
                var executionAdvice = advice.Instance as IExecutionAdvice;
                if (executionAdvice != null)
                {
                    this.executionAdvices.Add(executionAdvice);
                }
            }
        }

        public IList<IExecutionAdvice> ExecutionAdvices
        {
            get
            {
                return this.executionAdvices;
            }
        }
    }
}

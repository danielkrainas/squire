namespace Squire.Decoupled.Aspects
{
    using Squire.Validation;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Advice
    {
        public const int DefaultOrder = -1;

        public Advice(IAdvice instance, AdviceScope scope, int? order)
        {
            instance.VerifyParam("instance").IsNotNull();
            this.Order = order ?? Advice.DefaultOrder;
            this.Instance = instance;
            this.Scope = scope;
        }

        public IAdvice Instance
        {
            get;
            protected set;
        }

        public int Order
        {
            get;
            protected set;
        }

        public AdviceScope Scope
        {
            get;
            protected set;
        }
    }
}

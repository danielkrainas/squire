namespace Squire.Decoupled.Aspects
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public static class GlobalAdvice
    {
        static GlobalAdvice()
        {

        }

        public static GlobalAdviceCollection Advice
        {
            get;
            private set;
        }
    }
}

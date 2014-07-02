namespace Squire.Setup
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Squire.Validation;

    public class AdvancedAppSetup<TApp, TAdvanced> : IAdvancedAppSetup<TApp, TAdvanced>
    {
        private readonly TAdvanced advanced;

        private readonly IAppSetup<TApp> setup;

        public AdvancedAppSetup(IAppSetup<TApp> setup, TAdvanced advanced)
        {
            setup.VerifyParam("setup").IsNotNull();
            this.advanced = advanced;
            this.setup = setup;
        }

        public IAppSetup<TApp> AndThen
        {
            get
            {
                return this.setup;
            }
        }

        public TAdvanced Advanced
        {
            get
            {
                return this.advanced;
            }
        }
    }
}

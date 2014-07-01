namespace Squire.Setup
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Squire.Validation;

    public class AdvancedAppSetup<TAdvanced> : IAdvancedAppSetup<TAdvanced>
    {
        private readonly IAppSetup setup;

        private readonly TAdvanced advanced;

        public AdvancedAppSetup(IAppSetup setup, TAdvanced advanced)
        {
            setup.VerifyParam("setup").IsNotNull();
            this.advanced = advanced;
            this.setup = setup;
        }

        public IAppSetup AndThen
        {
            get
            {
                return setup;
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

    public class AdvancedAppSetup<TApp, TAdvanced> : AdvancedAppSetup<TAdvanced>, IAdvancedAppSetup<TApp, TAdvanced>
    {
        private readonly IAppSetup<TApp> setup;

        public AdvancedAppSetup(IAppSetup<TApp> setup, TAdvanced advanced)
            : base(setup, advanced)
        {
            this.setup = setup;
        }

        public new IAppSetup<TApp> AndThen
        {
            get
            {
                return this.setup;
            }
        }
    }
}

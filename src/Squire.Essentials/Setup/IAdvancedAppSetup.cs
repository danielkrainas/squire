namespace Squire.Setup
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public interface IAdvancedAppSetup<out TAdvanced>
    {
        IAppSetup AndThen
        {
            get;
        }

        TAdvanced Advanced
        {
            get;
        }
    }

    public interface IAdvancedAppSetup<out TApp, out TAdvanced> : IAdvancedAppSetup<TAdvanced>
    {
        new IAppSetup<TApp> AndThen
        {
            get;
        }
    }
}

namespace Squire.Setup
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public static class GenericSetupExtensions
    {
        public static IAppSetup<T> Setup<T>(this T application)
        {
            return new AppSetup<T>(application);
        }
    }
}

namespace Squire.Sentinel.Authentication
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public interface IPlayerRegistrar
    {
        void Register(IPlayer player);
    }
}

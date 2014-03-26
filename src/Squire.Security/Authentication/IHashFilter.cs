namespace Squire.Sentinel.Authentication
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public interface IHashFilter
    {
        string Calculate(string playerName, string hashTarget);
    }
}

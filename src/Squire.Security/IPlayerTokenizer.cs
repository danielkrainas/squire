namespace Squire.Security
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public interface IPlayerTokenizer
    {
        IPlayer Redeem(string token);

        string GetToken(IPlayer token);
    }
}

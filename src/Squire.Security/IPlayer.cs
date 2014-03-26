namespace Squire.Sentinel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Principal;
    using System.Text;

    public interface IPlayer : IIdentity
    {
        string Id
        {
            get;
        }

        string Password
        {
            get;
        }

        string Email
        {
            get;
            set;
        }

        bool IsDisabled
        {
            get;
            set;
        }

        DateTime CreatedOn
        {
            get;
        }
    }
}

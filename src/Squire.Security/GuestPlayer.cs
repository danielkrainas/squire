namespace Squire.Sentinel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class GuestPlayer : IPlayer
    {
        public GuestPlayer()
        {
            this.Id = Guid.NewGuid().ToString();
            this.CreatedOn = DateTime.Now;
        }

        public string Id
        {
            get;
            private set;
        }

        public string Name
        {
            get
            {
                return "guest";
            }
        }

        public string Password
        {
            get
            {
                return "";
            }
        }

        public string Email
        {
            get
            {
                return "";
            }
            set
            {
                throw new NotSupportedException("guest accounts do not have emails");
            }
        }

        public bool IsDisabled
        {
            get
            {
                return false;
            }
            set
            {
                throw new NotSupportedException("guest accounts cannot be disabled");
            }
        }

        public string AuthenticationType
        {
            get
            {
                return "none";
            }
        }

        public bool IsAuthenticated
        {
            get
            {
                return false;
            }
        }

        public DateTime CreatedOn
        {
            get;
            private set;
        }
    }
}

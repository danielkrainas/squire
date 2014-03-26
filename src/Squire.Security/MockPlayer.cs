namespace Squire.Sentinel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Principal;
    using System.Text;

    public class MockPlayer : IPlayer
    {
        private MockPlayer()
        {
            this.Id = Guid.NewGuid();
            this.CreatedOn = DateTime.UtcNow;
        }

        public MockPlayer(string email, string name = null)
            : this()
        {
            this.Email = email;
            this.Name = name ?? email;
        }

        public MockPlayer(Guid id, string email, string name = null)
            : this(email, name: name)
        {
            this.Id = id;
        }

        public Guid Id
        {
            get;
            set;
        }

        string IPlayer.Id
        {
            get
            {
                return this.Id.ToString();
            }
        }

        public string Email
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public string Password
        {
            get;
            set;
        }

        public bool IsDisabled
        {
            get;
            set;
        }

        public DateTime CreatedOn
        {
            get;
            set;
        }

        public string AuthenticationType
        {
            get;
            set;
        }

        public bool IsAuthenticated
        {
            get;
            set;
        }
    }
}

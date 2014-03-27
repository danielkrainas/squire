namespace Squire.Sentinel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Squire.Validation;
    using Squire.Decoupled.Commands;
    using System.Security.Cryptography;

    public class RegistrationDetails
    {
        public RegistrationDetails(string name, string email, string password = null)
        {
            name.VerifyParam("name").IsNotNull();
            email.VerifyParam("email").IsNotNull();
            this.Name = name;
            this.Email = email;
            this.Password = password;
        }

        public string Name
        {
            get;
            private set;
        }

        public string Email
        {
            get;
            private set;
        }

        public string Password
        {
            get;
            set;
        }
    }
}

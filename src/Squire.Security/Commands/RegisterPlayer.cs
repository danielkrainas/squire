namespace Squire.Sentinel.Commands
{
    using Squire.Unhinged.Commands;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Squire.Validation;

    public class RegisterPlayer : CommandBase
    {
        public RegisterPlayer(string name, string email, string passwordHash)
        {
            name.VerifyParam("name").IsNotNull();
            email.VerifyParam("email").IsNotNull();
            passwordHash.VerifyParam("password").IsNotNull();
            this.Name = name;
            this.Email = email;
            this.PasswordHash = passwordHash;
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

        public string PasswordHash
        {
            get;
            private set;
        }
    }
}

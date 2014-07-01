namespace Squire.Security.Queries
{
    using Squire.Decoupled.Queries;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Squire.Validation;

    public class GetPlayerByEmail : IQuery<IPlayer>
    {
        public GetPlayerByEmail(string email)
        {
            email.VerifyParam("email").IsNotBlank();
            this.Email = email;
        }

        public string Email
        {
            get;
            private set;
        }
    }
}

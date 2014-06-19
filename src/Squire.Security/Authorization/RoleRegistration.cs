namespace Squire.Security.Authorization
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Squire.Validation;

    public class RoleRegistration
    {
        public RoleRegistration(string name)
        {
            name.VerifyParam("name").IsNotNull();
            this.Name = name;
        }

        public string Name
        {
            get;
            private set;
        }
    }
}

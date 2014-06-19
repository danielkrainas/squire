namespace Squire.Security.Authorization
{
    using Squire.Validation;
    using System;

    public class SimpleRole : IRole
    {
        public SimpleRole(string name)
        {
            name.VerifyParam("name").IsNotNull();
            this.Id = name;
            this.Name = name;
        }

        public SimpleRole()
            : this("")
        {
        }

        public virtual string Id
        {
            get;
            protected set;
        }

        public virtual string Name
        {
            get;
            protected set;
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }

        public override string ToString()
        {
            return this.Name;
        }

        public override bool Equals(object obj)
        {
            return !Object.ReferenceEquals(obj, null) && this.Equals(obj as SimpleRole);
        }

        public virtual bool Equals(SimpleRole role)
        {
            return !Object.ReferenceEquals(role, null) && Object.ReferenceEquals(this, role) || this.Name.Equals(role.Name, StringComparison.InvariantCulture);
        }
    }
}

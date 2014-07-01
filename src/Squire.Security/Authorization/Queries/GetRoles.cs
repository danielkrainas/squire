namespace Squire.Security.Authorization.Queries
{
    using Squire.Decoupled.Queries;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class GetRoles : QueryWithConditions<IRole, ICollection<IRole>>
    {
        public GetRoles()
        {
            this.NameFilter = null;
        }

        public string NameFilter
        {
            get;
            private set;
        }

        public bool HasNameFilter
        {
            get
            {
                return this.NameFilter != null;
            }
        }

        public GetRoles DontFilter()
        {
            this.NameFilter = null;
            return this;
        }

        public GetRoles WithNameLike(string filter)
        {
            this.NameFilter = filter;
            return this;
        }
    }
}

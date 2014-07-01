namespace Squire.Security.Queries
{
    using Squire.Decoupled.Queries;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class GetPlayers : QueryWithConditions<IPlayer, ICollection<IPlayer>>
    {
        public GetPlayers()
        {
            this.Name = null;
        }

        public string Name
        {
            get;
            private set;
        }

        public bool HasNameFilter
        {
            get
            {
                return this.Name != null;
            }
        }

        public string Email
        {
            get;
            private set;
        }

        public bool HasEmailFilter
        {
            get
            {
                return this.Email != null;
            }
        }

        public GetPlayers WithNameLike(string filter)
        {
            this.Name = filter;
            return this;
        }

        public GetPlayers AnyName()
        {
            this.Name = null;
            return this;
        }

        public GetPlayers WithEmailLike(string filter)
        {
            this.Email = filter;
            return this;
        }

        public GetPlayers AnyEmail()
        {
            this.Email = null;
            return this;
        }
    }
}

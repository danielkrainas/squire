namespace Squire.Security.Authorization
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Squire.Validation;

    public class DevAuthorizationStrategy : AuthorizationStrategyAdapter
    {
        public DevAuthorizationStrategy(bool readOnly)
            : base(new SimpleRoleResolver(), new InMemoryRoleTracker(), readOnly ? null : new InMemoryRoleRegistrar())
        {
            this.IsReadOnly = readOnly;
        }

        public DevAuthorizationStrategy(bool readOnly, IPlayer player, params string[] roleNames)
            : this(readOnly)
        {
            player.VerifyParam("player").IsNotNull();
            roleNames.VerifyParam("roleNames").IsNotNull();
            foreach (var roleName in roleNames)
            {
                this.AddPlayer(player, this.Select(roleName));
            }
        }

        public DevAuthorizationStrategy(bool readOnly, IDictionary<IPlayer, IEnumerable<string>> playerAssignments)
            : this(readOnly)
        {
            if (playerAssignments != null && playerAssignments.Any())
            {
                foreach (var assignment in playerAssignments)
                {
                    foreach(var roleName in assignment.Value)
                    {
                        this.AddPlayer(assignment.Key, this.Select(roleName));
                    }
                }
            }
        }

        public bool IsReadOnly
        {
            get;
            private set;
        }
    }
}

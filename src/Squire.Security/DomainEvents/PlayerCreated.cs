namespace Squire.Sentinel.DomainEvents
{
    using Squire.Unhinged.DomainEvents;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class PlayerCreated : DomainEventBase
    {
        public PlayerCreated(IPlayer player)
        {
            this.Player = player;
        }

        public IPlayer Player
        {
            get;
            set;
        }
    }
}

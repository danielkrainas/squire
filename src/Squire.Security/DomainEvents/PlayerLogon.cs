namespace Squire.Sentinel.DomainEvents
{
    using Squire.Decoupled.DomainEvents;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class PlayerLogon : DomainEventBase
    {
        public PlayerLogon(IPlayer player)
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

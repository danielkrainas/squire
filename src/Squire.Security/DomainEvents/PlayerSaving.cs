namespace Squire.Sentinel.DomainEvents
{
    using Squire.Unhinged.DomainEvents;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class PlayerSaving : DomainEventBase
    {
        public PlayerSaving(IPlayer player)
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

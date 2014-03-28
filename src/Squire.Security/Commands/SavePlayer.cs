namespace Squire.Security.Commands
{
    using Squire.Decoupled.Commands;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class SavePlayer : CommandBase
    {
        public SavePlayer(IPlayer player)
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

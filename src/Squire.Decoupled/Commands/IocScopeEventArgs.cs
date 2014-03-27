namespace Squire.Decoupled.Commands
{
    using Microsoft.Practices.ServiceLocation;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class IocScopeEventArgs : EventArgs
    {
        public IocScopeEventArgs(IServiceLocator childScope, ICommand command)
        {
            this.ChildScope = childScope;
            this.Command = command;
        }

        public IServiceLocator ChildScope
        {
            get;
            private set;
        }

        public ICommand Command
        {
            get;
            set;
        }
    }
}

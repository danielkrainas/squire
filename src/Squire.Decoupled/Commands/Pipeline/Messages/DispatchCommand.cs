﻿namespace Squire.Unhinged.Commands.Pipeline.Messages
{
    using Squire.Validation;
    using Squire.Unhinged.Pipeline;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class DispatchCommand : IDownstreamMessage
    {
        public DispatchCommand(ICommand command, int attempts)
            : this(command)
        {
            this.Attempts = attempts;
        }

        public DispatchCommand(ICommand command)
        {
            ValidationHelper.ArgumentNotNull(command, "command");
            this.Command = command;
            this.Attempts = 0;
        }

        protected DispatchCommand()
        {
            this.Command = null;
            this.Attempts = 0;
        }

        public ICommand Command
        {
            get;
            set;
        }

        public int Attempts
        {
            get;
            private set;
        }

        public void AddFailedAttempt()
        {
            this.Attempts++;
        }
    }
}

﻿namespace Squire.Security.Authentication
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public interface IValidator
    {
        bool Validate(IPlayer player, string attemptedHash);
    }
}

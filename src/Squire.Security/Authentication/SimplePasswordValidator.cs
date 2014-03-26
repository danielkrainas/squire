namespace Squire.Sentinel.Authentication
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class SimplePasswordValidator : IValidator
    {
        public bool Validate(IPlayer player, string attemptedHash)
        {
            if (player != null && player.Password != null && attemptedHash != null && player.Password.Equals(attemptedHash))
            {
                return true;
            }

            return false;
        }
    }
}

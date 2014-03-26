namespace Squire.Sentinel.Authentication
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public interface IAuthenticationStrategy
    {
        IPlayer Validate(string name, string password);

        void StartSession(IPlayer player, bool persist);

        void ResumeSession();

        void EndSession();

        IPlayer Register(RegistrationDetails registration);
    }
}

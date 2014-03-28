namespace Squire.Security.Abilities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public interface IAbilityDispatcher
    {
        bool Check<TAbility>(IPlayer player)
            where TAbility : class, IAbility;

        bool Check<TAbility, TObject>(IPlayer player, TObject obj)
            where TAbility : class, IAbility;
    }
}

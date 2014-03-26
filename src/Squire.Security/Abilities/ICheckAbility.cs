namespace Squire.Sentinel.Abilities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public interface ICheckAbility<TAbility>
    where TAbility : class, IAbility
    {
        bool Check(IPlayer player);
    }

    public interface ICheckAbility<TAbility, TObject>
        where TAbility : class, IAbility
    {
        bool Check(IPlayer player, TObject obj);
    }
}

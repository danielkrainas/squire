namespace Squire.Sentinel.Abilities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public static class Ability
    {
        private static IAbilityDispatcher _dispatcher;

        public static void Assign(IAbilityDispatcher dispatcher)
        {
            Ability._dispatcher = dispatcher;
        }

        public static bool Check<TAbility>(IPlayer player)
            where TAbility : class, IAbility
        {
            if (Ability._dispatcher == null)
            {
                throw new InvalidOperationException("the default dispatcher has not been specified");
            }

            return Ability._dispatcher.Check<TAbility>(player);
        }

        public static bool Check<TAbility, TObject>(IPlayer player, TObject obj)
            where TAbility : class, IAbility
        {
            if (Ability._dispatcher == null)
            {
                throw new InvalidOperationException("the default dispatcher has not been specified");
            }

            return Ability._dispatcher.Check<TAbility, TObject>(player, obj);
        }
    }
}

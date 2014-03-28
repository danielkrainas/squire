namespace Squire.Security.Abilities
{
    using Microsoft.Practices.ServiceLocation;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using Squire.Validation;

    public class IocAbilityDispatcher : IAbilityDispatcher
    {
        private static readonly MethodInfo _checkSimpleMethod;

        private static readonly MethodInfo _checkTargetedMethod;

        private readonly IServiceLocator locator;

        static IocAbilityDispatcher()
        {
            IocAbilityDispatcher._checkSimpleMethod = typeof(IocAbilityDispatcher).GetMethod("InternalCheckSimple", BindingFlags.NonPublic | BindingFlags.Instance);
            IocAbilityDispatcher._checkTargetedMethod = typeof(IocAbilityDispatcher).GetMethod("InternalTargetedCheck", BindingFlags.NonPublic | BindingFlags.Instance);
        }

        public IocAbilityDispatcher(IServiceLocator locator)
        {
            locator.VerifyParam("locator").IsNotNull();
            this.locator = locator;
        }

        public bool Check<TAbility>(IPlayer player)
            where TAbility : class, IAbility
        {
            var genericExecute = IocAbilityDispatcher._checkSimpleMethod.MakeGenericMethod(typeof(TAbility));
            return (bool)genericExecute.Invoke(this, new object[] { player });
        }

        public bool Check<TAbility, TObject>(IPlayer player, TObject obj)
            where TAbility : class, IAbility
        {
            var genericExecute = IocAbilityDispatcher._checkTargetedMethod.MakeGenericMethod(typeof(TAbility), obj.GetType());
            return (bool)genericExecute.Invoke(this, new object[] { player, obj });
        }

        protected bool InternalCheckSimple<TAbility>(IPlayer player)
            where TAbility : class, IAbility
        {
            var handlerType = typeof(ICheckAbility<TAbility>);
            var handler = (ICheckAbility<TAbility>)this.locator.GetService(handlerType);
            return handler.Check(player);
        }

        protected bool InternalTargetedCheck<TAbility, TObject>(IPlayer player, TObject obj)
            where TAbility : class, IAbility
        {
            var handlerType = typeof(ICheckAbility<TAbility, TObject>);
            var handler = (ICheckAbility<TAbility, TObject>)this.locator.GetService(handlerType);
            return handler.Check(player, obj);
        }
    }
}

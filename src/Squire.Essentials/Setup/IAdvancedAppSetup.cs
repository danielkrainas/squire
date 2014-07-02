namespace Squire.Setup
{
    public interface IAdvancedAppSetup<out TApp, out TAdvanced>
    {
        IAppSetup<TApp> AndThen
        {
            get;
        }

        TAdvanced Advanced
        {
            get;
        }
    }
}

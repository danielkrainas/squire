namespace Squire.Setup
{
    public interface IAppSetup
    {
        object Application
        {
            get;
        }
    }

    public interface IAppSetup<out T> : IAppSetup
    {
        new T Application
        {
            get;
        }
    }
}

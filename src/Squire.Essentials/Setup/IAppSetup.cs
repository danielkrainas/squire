namespace Squire.Setup
{
    public interface IAppSetup<out T>
    {
        T Application
        {
            get;
        }
    }
}

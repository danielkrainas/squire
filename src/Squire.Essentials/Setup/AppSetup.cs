namespace Squire.Setup
{
    public class AppSetup<T> : IAppSetup<T>
    {
        private readonly T application;

        public AppSetup(T application)
        {
            this.application = application;
        }

        public T Application
        {
            get
            {
                return this.application;
            }
        }

        object IAppSetup.Application
        {
            get
            {
                return (object)this.application;
            }
        }
    }
}

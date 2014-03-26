namespace Squire.Unhinged.Practices
{
    using Squire.Practices;
    using Squire.Validation;
    using Microsoft.Practices.ServiceLocation;

    /// <summary>
    /// A factory implementation powered by a service locator.
    /// </summary>
    /// <typeparam name="T">The type of service the factory will create.</typeparam>
    public class IocFactory<T> : IFactory<T>
    {
        public readonly IServiceLocator locator;

        public IocFactory(IServiceLocator locator)
        {
            locator.VerifyParam("locator").IsNotNull();
            this.locator = locator;
        }

        public T Create()
        {
            return this.locator.GetInstance<T>();
        }
    }
}

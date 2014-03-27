namespace Squire.Sentinel.Queries
{
    using Squire.Decoupled.Queries;
    using Squire.Validation;

    public class GetPlayerByName : IQuery<IPlayer>
    {
        public GetPlayerByName(string name)
        {
            name.VerifyParam("name").IsNotBlank();
            this.Name = name;
        }

        public string Name
        {
            get;
            private set;
        }
    }
}

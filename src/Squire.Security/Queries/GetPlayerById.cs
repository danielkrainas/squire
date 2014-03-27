namespace Squire.Sentinel.Queries
{
    using Squire.Decoupled.Queries;
    using Squire.Validation;

    public class GetPlayerById : IQuery<IPlayer>
    {
        public GetPlayerById(string id)
        {
            id.VerifyParam("id").IsNotBlank();
            this.Id = id;
        }

        public string Id
        {
            get;
            private set;
        }
    }
}



using MongoDB.Driver;
using LeoMongo;
using LeoMongo.Database;


#nullable enable
namespace MongoDBDemoApp.Core.Util
{
    public sealed class DatabaseProvider : IDatabaseProvider
    {
        private readonly MongoClient _client;

        public DatabaseProvider(IMongoConfig options)
        {
            this._client = new MongoClient(options.ConnectionString);
            this.Database = this._client.GetDatabase(options.DatabaseName, (MongoDatabaseSettings) null!);
        }

        public IMongoDatabase Database { get; }

        public Task<IClientSessionHandle> StartSession() => this._client.StartSessionAsync((ClientSessionOptions) null!, new CancellationToken());
    }
}

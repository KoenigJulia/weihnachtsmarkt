using MongoDB.Driver;

namespace MongoDBDemoApp.Core.Util;

public interface ISessionProvider
{
    IClientSessionHandle Session { get; }
}
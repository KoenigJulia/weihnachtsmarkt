using LeoMongo.Database;
using MongoDB.Bson;

namespace MongoDBDemoApp.Core.Workloads.Places;

public sealed class Place: EntityBase
{
    public int PlaceNr { get; set; }
    public ObjectId? VendorId { get; set; }
}
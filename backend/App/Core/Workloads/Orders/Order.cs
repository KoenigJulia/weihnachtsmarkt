using LeoMongo.Database;
using MongoDB.Bson;
using MongoDBDemoApp.Core.Workloads.Products;

namespace MongoDBDemoApp.Core.Workloads.Orders;

public class Order : EntityBase
{
    public string Name { get; set; } = String.Empty;
    public DateTime Created { get; set; }
    public ObjectId? CustomerId { get; set; }
    public List<ObjectId> Products { get; set; } = new();
}
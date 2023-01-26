using LeoMongo.Database;
using MongoDB.Bson;
using MongoDBDemoApp.Core.Workloads.Products;

namespace MongoDBDemoApp.Core.Workloads.Orders;

public class Order : EntityBase
{
    public DateTime Created { get; set; }
    public ObjectId? CustomerId { get; set; }
    public List<ObjectId> OrderItems { get; set; } = new();
}
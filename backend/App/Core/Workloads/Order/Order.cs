using LeoMongo.Database;
using MongoDB.Bson;

namespace MongoDBDemoApp.Core.Workloads.Order;

public class Order : EntityBase
{
    public DateTime Created { get; set; }
    public ObjectId? CustomerId { get; set; }
    public List<OrderItem> OrderItems { get; set; } = new();
}
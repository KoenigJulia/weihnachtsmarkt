using LeoMongo.Database;
using MongoDB.Bson;

namespace MongoDBDemoApp.Core.Workloads.Orders;

public class OrderItem
{
    public ObjectId? OrderId { get; set; }
    public ObjectId? ProductId { get; set; }
}
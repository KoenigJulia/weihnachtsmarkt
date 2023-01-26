using MongoDB.Bson;

namespace MongoDBDemoApp.Model.Order;

public class CreateOrderRequest
{
    public string CustomerId { get; set; } = default!;
    public List<string> OrderItems { get; set; } = new();
}
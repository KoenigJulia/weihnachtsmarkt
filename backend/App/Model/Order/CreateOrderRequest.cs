using MongoDB.Bson;

namespace MongoDBDemoApp.Model.Order;

public class CreateOrderRequest
{
    public string Name { get; set; } = default!;
    public string CustomerId { get; set; } = default!;
    public List<string> Products { get; set; } = new();
}
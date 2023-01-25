using LeoMongo.Database;

namespace MongoDBDemoApp.Core.Workloads.Products;

public sealed class Product: EntityBase
{
    public string Name { get; set; } = default!;
    public float Price { get; set; }
}
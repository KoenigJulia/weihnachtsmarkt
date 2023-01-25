using LeoMongo.Database;
using MongoDB.Bson;
using MongoDBDemoApp.Core.Workloads.Products;

namespace MongoDBDemoApp.Core.Workloads.Vendors;

public sealed class Vendor: EntityBase
{
    public string Name { get; set; } = default!;
    public List<ObjectId> Products { get; set; } = new List<ObjectId>();
    public List<Employee> Employees { get; set; } = new List<Employee>();
}
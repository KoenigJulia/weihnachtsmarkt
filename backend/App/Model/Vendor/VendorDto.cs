using MongoDB.Bson;
using MongoDBDemoApp.Core.Workloads.Vendors;

namespace MongoDBDemoApp.Model.Vendor;

public class VendorDto
{
    public string? Id { get; set; } = default!;
    public string Name { get; set; } = default!;
    public List<ObjectId> Products { get; set; } = new List<ObjectId>();
    public List<Employee> Employees { get; set; } = new List<Employee>();
}
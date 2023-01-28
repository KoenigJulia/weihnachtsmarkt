using MongoDBDemoApp.Core.Workloads.Vendors;

namespace MongoDBDemoApp.Model.Vendor;

public class VendorDto
{
    public string? Id { get; set; } = default!;
    public string Name { get; set; } = default!;
    public List<string> Products { get; set; } = new();
    public List<Employee> Employees { get; set; } = new();
}
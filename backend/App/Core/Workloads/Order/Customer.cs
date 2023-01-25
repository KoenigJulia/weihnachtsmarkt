using LeoMongo.Database;

namespace MongoDBDemoApp.Core.Workloads.Order;

public class Customer : EntityBase
{
    public String FirstName { get; set; } = default!;
    public String LastName { get; set; } = default!;
    public String PhoneNumber { get; set; } = default!;
}
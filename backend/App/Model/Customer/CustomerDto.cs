namespace MongoDBDemoApp.Model.CreateCustomerRequest;

public class CustomerDto
{
    public string? Id { get; set; } = default!;
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
}
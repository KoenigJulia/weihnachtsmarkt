namespace MongoDBDemoApp.Model.CreateCustomerRequest;

public class CreateCustomerRequest
{
    public String FirstName { get; set; } = default!;
    public String LastName { get; set; } = default!;
    public String PhoneNumber { get; set; } = default!;
}
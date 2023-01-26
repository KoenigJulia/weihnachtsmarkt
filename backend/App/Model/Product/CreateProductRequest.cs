namespace MongoDBDemoApp.Model.Product;

public class CreateProductRequest
{
    public string Name { get; set; } = default!;
    public float Price { get; set; } = default!;
    public string VendorId { get; set; } = default!;
}
namespace MongoDBDemoApp.Model.Product;

public class ProductDto
{
    public string? Id { get; set; } = default!;
    public string Name { get; set; } = default!;
    public float Price { get; set; }
}
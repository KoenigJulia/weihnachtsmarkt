using MongoDB.Bson;

namespace MongoDBDemoApp.Core.Workloads.Products;

public class ProductService: IProductService
{
    private readonly IProductRepository _repository;

    public ProductService(ProductRepository productRepository)
    {
        _repository = productRepository;
    }
    
    public Task<IReadOnlyCollection<Product>> GetAllProducts()
    {
        return _repository.GetAllProducts();
    }

    public Task<Product?> GetProductById(ObjectId id)
    {
        return _repository.GetProductById(id);
    }

    public Task<Product> AddProduct(string name, float price, ObjectId vendorId)
    {
        Product product = new Product()
        {
            Name = name,
            Price = price
        };
        return _repository.AddProduct(product);
    }

    public Task DeleteProduct(ObjectId id)
    {
        return _repository.DeleteProduct(id);
    }
}
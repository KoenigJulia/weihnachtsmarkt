using MongoDB.Bson;
using MongoDBDemoApp.Core.Workloads.Vendors;

namespace MongoDBDemoApp.Core.Workloads.Products;

public class ProductService: IProductService
{
    private readonly IProductRepository _repository;
    private readonly IVendorRepository _vendorRepository;

    public ProductService(IProductRepository productRepository, IVendorRepository vendorRepository)
    {
        _repository = productRepository;
        _vendorRepository = vendorRepository;
    }
    
    public Task<IReadOnlyCollection<Product>> GetAllProducts()
    {
        return _repository.GetAllProducts();
    }

    public Task<Product?> GetProductById(ObjectId id)
    {
        return _repository.GetProductById(id);
    }

    public async Task<Product> AddProduct(string name, float price, ObjectId vendorId)
    {
        Product product = new Product()
        {
            Name = name,
            Price = price
        };
        var p = await _repository.AddProduct(product);
        await _vendorRepository.AddProductToVendor(p.Id, vendorId);
        return p;
    }

    public Task DeleteProduct(ObjectId id)
    {
        return _repository.DeleteProduct(id);
    }
}
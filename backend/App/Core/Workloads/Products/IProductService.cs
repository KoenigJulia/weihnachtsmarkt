using MongoDB.Bson;

namespace MongoDBDemoApp.Core.Workloads.Products;

public interface IProductService
{
    Task<IReadOnlyCollection<Product>> GetAllProducts();
    Task<Product?> GetProductById(ObjectId id);
    Task<Product> AddProduct(string name, float price, ObjectId vendorId);
    Task DeleteProduct(ObjectId id);
}
using LeoMongo.Database;
using MongoDB.Bson;

namespace MongoDBDemoApp.Core.Workloads.Products;

public interface IProductRepository: IRepositoryBase
{
    Task<Product> AddProduct(Product product);
    Task<IReadOnlyCollection<Product>> GetAllProducts();
    Task<Product?> GetProductById(ObjectId id);
    Task DeleteProduct(ObjectId id);
}
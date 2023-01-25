using LeoMongo;
using LeoMongo.Database;
using LeoMongo.Transaction;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace MongoDBDemoApp.Core.Workloads.Products;

public sealed class ProductRepository: RepositoryBase<Product>, IProductRepository
{
    public ProductRepository(ITransactionProvider transactionProvider, IDatabaseProvider databaseProvider) : base(transactionProvider, databaseProvider)
    {
    }

    public override string CollectionName { get; } = MongoUtil.GetCollectionName<Product>();
    
    public Task<Product> AddProduct(Product product)
    {
        return InsertOneAsync(product);
    }

    public async Task<IReadOnlyCollection<Product>> GetAllProducts()
    {
        return await Query().ToListAsync();
    }

    public async Task<Product?> GetProductById(ObjectId id)
    {
        return await Query().SingleOrDefaultAsync(p => p.Id == id);
    }

    public Task DeleteProduct(ObjectId id)
    {
        return DeleteOneAsync(id);
    }
}
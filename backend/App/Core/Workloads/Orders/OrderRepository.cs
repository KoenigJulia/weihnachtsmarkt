using LeoMongo;
using LeoMongo.Database;
using LeoMongo.Transaction;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MongoDBDemoApp.Core.Workloads.Products;

namespace MongoDBDemoApp.Core.Workloads.Orders;

public sealed class OrderRepository : RepositoryBase<Order>, IOrderRepository
{
    private readonly IProductRepository _productRepository;
    private readonly IDatabaseProvider _databaseProvider;
    
    private IMongoCollection<TCollection> GetCollection<TCollection>(string collectionName) => this._databaseProvider.Database.GetCollection<TCollection>(collectionName);
    
    public OrderRepository(ITransactionProvider transactionProvider, IDatabaseProvider databaseProvider, 
        IProductRepository productRepository) : base(
        transactionProvider, databaseProvider)
    {
        _databaseProvider = databaseProvider;
        _productRepository = productRepository;
    }

    public override string CollectionName { get; } = MongoUtil.GetCollectionName<Order>();

    

    /*public async Task<IReadOnlyCollection<Comment>> GetUnapprovedComments()
    {
        return await Query().Where(x => x.Approved == false).ToListAsync();
    }*/

    /*public async Task<bool> ApproveComment(ObjectId id)
    {
        var x = UpdateDefBuilder.Set(x => x.Approved, true);
        var res = await UpdateOneAsync(id, x);
        return res is { IsAcknowledged: true, ModifiedCount: 1 };
    }*/

    public async Task<bool> DeleteProduct(ObjectId id)
    {
        var deleteResult = await DeleteOneAsync(id);
        return deleteResult.DeletedCount == 1;
    }

    public async Task<Order> AddOrder(Order order)
    {
        return await InsertOneAsync(order);
    }

    public async Task<IReadOnlyCollection<Order>> GetOrders()
    {
        return await Query().ToListAsync();
    }

    public async Task<Order?> GetOrderById(ObjectId id)
    {
        return await Query().Where(o => o.Id == id).FirstOrDefaultAsync();
    }

    public async Task<bool> DeleteOrder(ObjectId id)
    {
        var deleteResult = await DeleteOneAsync(id);
        return deleteResult.DeletedCount == 1;
    }

    public async Task<IReadOnlyCollection<Product>> GetProductsForOrder(ObjectId orderId)
    {
        var productIds = await Query().Where(o => o.Id == orderId).
            Select(o => o.Products).FirstOrDefaultAsync();
        var list = new List<Product>();
        foreach (var productId in productIds)
        {
            var product = await _productRepository.GetProductById(productId);
            if (product != null) list.Add(product);
        }
        return list;
    }

    public async Task<bool> AddProduct(ObjectId orderId, ObjectId product)
    {
        var x = UpdateDefBuilder.Push(o => o.Products, product);
        var res = await UpdateOneAsync(orderId, x);
        return res is { IsAcknowledged: true, ModifiedCount: 1 };
    }


    public async Task<bool> DeleteProductOfOrder(ObjectId orderId, ObjectId product)
    {
        Order? order = await GetOrderById(orderId);
        if (order == default)
        {
            return await Task.FromResult(false);
        }

        bool removalSucceeded = order.Products.Remove(product);
        if (removalSucceeded == false)
        {
            return await Task.FromResult(false);
        }

        ReplaceOneResult? res = await ReplaceOneAsync(order);
        return res is { IsAcknowledged: true, ModifiedCount: 1 };
    }

    public async Task<bool> DeleteProductsOfOrder(ObjectId orderId)
    {
        Order? order = await GetOrderById(orderId);
        if (order == default)
        {
            return await Task.FromResult(false);
        }

        order.Products.Clear();

        ReplaceOneResult? res = await ReplaceOneAsync(order);
        return res is { IsAcknowledged: true, ModifiedCount: 1 };
    }

    public async Task<float> GetOrderPrice(ObjectId orderId)
    {
        var q = (await Query().Where(o => o.Id == orderId).Select(o => o.OrderItems).FirstOrDefaultAsync()!).Join(
            (await this.GetCollection<Product>(_productRepository.CollectionName).AsQueryable().ToListAsync()),
            o => o, 
            p => p.Id, 
            (order, product) => (product.Price)).Sum();
        return q;
    }

    public async Task<bool> DeleteOrdersOfCustomer(ObjectId customerId)
    {
        var res = await DeleteManyAsync(o => o.CustomerId == customerId);
        return res.IsAcknowledged;
    }
    
    
}
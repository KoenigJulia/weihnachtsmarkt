using LeoMongo;
using LeoMongo.Database;
using LeoMongo.Transaction;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MongoDBDemoApp.Core.Workloads.Orders;
using MongoDBDemoApp.Core.Workloads.Products;

namespace MongoDBDemoApp.Core.Workloads.Vendors;

public class VendorRepository : RepositoryBase<Vendor>, IVendorRepository
{
    private readonly IOrderRepository _orderRepository;
    private readonly IDatabaseProvider _databaseProvider;

    private IMongoCollection<TCollection> GetCollection<TCollection>(string collectionName) =>
        this._databaseProvider.Database.GetCollection<TCollection>(collectionName);

    public VendorRepository(ITransactionProvider transactionProvider, IDatabaseProvider databaseProvider,
        IOrderRepository orderRepository) : base(transactionProvider, databaseProvider)
    {
        _databaseProvider = databaseProvider;
        _orderRepository = orderRepository;
        AddUniqueNameIndex();
    }

    public override string CollectionName { get; } = MongoUtil.GetCollectionName<Vendor>();

    public Task<Vendor> AddVendor(Vendor vendor)
    {
        return InsertOneAsync(vendor);
    }

    public async Task<IReadOnlyCollection<Vendor>> GetAllVendors()
    {
        var x =await GetTotalEmployeeCount();
        return await Query().ToListAsync();
    }

    public async Task<Vendor?> GetVendorById(ObjectId id)
    {
        return await Query().SingleOrDefaultAsync(v => v.Id == id);
    }

    public Task DeleteVendor(ObjectId id)
    {
        return DeleteOneAsync(id);
    }

    public async Task<bool> AddProductToVendor(ObjectId productId, ObjectId vendorId)
    {
        var x = UpdateDefBuilder.Push(v => v.Products, productId);
        var res = await UpdateOneAsync(vendorId, x);
        return res is { IsAcknowledged: true, ModifiedCount: 1 };
    }

    public async Task<bool> AddEmployeeToVendor(Employee employee, ObjectId vendorId)
    {
        var x = UpdateDefBuilder.Push(v => v.Employees, employee);
        var res = await UpdateOneAsync(vendorId, x);
        return res is { IsAcknowledged: true, ModifiedCount: 1 };
    }

    public async Task<List<Employee>> GetAllEmployees()
    {
        return await Query().Select(v => v.Employees).SelectMany(x => x).ToListAsync();
    }

    public async Task<bool> DeleteProductFromVendor(ObjectId productId)
    {
        Vendor? vendor = await GetVendorForProduct(productId);
        if (vendor == default)
        {
            return await Task.FromResult(false);
        }

        bool removalSucceeded = vendor.Products.Remove(productId);
        if (removalSucceeded == false)
        {
            return await Task.FromResult(false);
        }

        ReplaceOneResult? res = await ReplaceOneAsync(vendor);
        return res is { IsAcknowledged: true, ModifiedCount: 1 };
    }

    private async Task<Vendor?> GetVendorForProduct(ObjectId productId)
    {
        return await Query().Where(vendor => vendor.Products.Contains(productId)).FirstOrDefaultAsync();
    }

    private async void AddUniqueNameIndex()
    {
        var indexOption = new CreateIndexOptions
        {
            Unique = true
        };
        var indexKeys = Builders<Vendor>.IndexKeys.Ascending(v => v.Name);
        var indexModel = new CreateIndexModel<Vendor>(indexKeys, indexOption);
        var col = GetCollection<Vendor>(CollectionName);
        await col.Indexes.CreateOneAsync(indexModel);
    }

    private async Task<int> GetTotalEmployeeCount()
    {
        return await Query().SumAsync(v => v.Employees.Count());
    }
}
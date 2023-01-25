using LeoMongo;
using LeoMongo.Database;
using LeoMongo.Transaction;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MongoDBDemoApp.Core.Workloads.Products;

namespace MongoDBDemoApp.Core.Workloads.Vendors;

public class VendorRepository: RepositoryBase<Vendor>, IVendorRepository
{
    
    public VendorRepository(ITransactionProvider transactionProvider, IDatabaseProvider databaseProvider) : base(transactionProvider, databaseProvider)
    {
    }

    public override string CollectionName { get; } = MongoUtil.GetCollectionName<Vendor>();

    public Task<Vendor> AddVendor(Vendor vendor)
    {
        return InsertOneAsync(vendor);
    }

    public async Task<IReadOnlyCollection<Vendor>> GetAllVendors()
    {
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
}
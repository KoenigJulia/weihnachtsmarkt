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
}
using LeoMongo.Database;
using MongoDB.Bson;
using MongoDBDemoApp.Core.Workloads.Products;

namespace MongoDBDemoApp.Core.Workloads.Vendor;

public interface IVendorRepository: IRepositoryBase
{
    Task<Vendor> AddVendor(Vendor vendor);
    Task<IReadOnlyCollection<Vendor>> GetAllVendors();
    Task<Vendor?> GetVendorById(ObjectId id);
    Task DeleteVendor(ObjectId id);
    
}
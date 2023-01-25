using MongoDB.Bson;

namespace MongoDBDemoApp.Core.Workloads.Vendors;

public interface IVendorService
{
    Task<IReadOnlyCollection<Vendor>> GetAllVendors();
    Task<Vendor?> GetVendorById(ObjectId id);
    Task<Vendor> AddVendor(string name, ObjectId placeId);
    Task DeleteVendor(ObjectId id);
}
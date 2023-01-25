using MongoDB.Bson;

namespace MongoDBDemoApp.Core.Workloads.Vendors;

public interface IVendorService
{
    Task<IReadOnlyCollection<Vendor>> GetAllVendors();
    Task<Vendor?> GetVendorById(ObjectId id);
    Task<Vendor> AddVendor(string name);
    Task DeleteVendor(ObjectId id);
    Task<bool> AddEmployeeToVendor(string firstName, string lastName, ObjectId vendorId);
}
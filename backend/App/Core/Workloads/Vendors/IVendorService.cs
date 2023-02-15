using MongoDB.Bson;

namespace MongoDBDemoApp.Core.Workloads.Vendors;

public interface IVendorService
{
    Task<IReadOnlyCollection<Vendor>> GetAllVendors();
    Task<Vendor?> GetVendorById(ObjectId id);
    Task<Vendor> AddVendor(string name);
    Task DeleteVendor(ObjectId id);
    Task<bool> AddEmployeeToVendor(Employee employee, ObjectId vendorId);
    Task<List<Employee>> GetAllEmployees();
    Task<int> GetTotalEmployeeCount();
}
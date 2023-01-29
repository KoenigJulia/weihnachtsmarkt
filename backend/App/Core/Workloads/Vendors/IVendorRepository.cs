using LeoMongo.Database;
using MongoDB.Bson;
using MongoDBDemoApp.Core.Workloads.Products;

namespace MongoDBDemoApp.Core.Workloads.Vendors;

public interface IVendorRepository: IRepositoryBase
{
    Task<Vendor> AddVendor(Vendor vendor);
    Task<IReadOnlyCollection<Vendor>> GetAllVendors();
    Task<Vendor?> GetVendorById(ObjectId id);
    Task DeleteVendor(ObjectId id);

    Task<bool> AddProductToVendor(ObjectId productId, ObjectId vendorId);
    Task<bool> AddEmployeeToVendor(Employee employee, ObjectId vendorId);
    Task<List<Employee>> GetAllEmployees();
    Task<bool> DeleteProductFromVendor(ObjectId produktId);
}
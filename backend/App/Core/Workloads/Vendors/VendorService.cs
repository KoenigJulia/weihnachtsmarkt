using MongoDB.Bson;
using MongoDBDemoApp.Core.Workloads.Places;

namespace MongoDBDemoApp.Core.Workloads.Vendors;

public sealed class VendorService : IVendorService
{
    private readonly IPlaceRepository _placeRepository;
    private readonly IVendorRepository _repository;

    public VendorService(IVendorRepository repository, IPlaceRepository placeRepository)
    {
        _repository = repository;
        _placeRepository = placeRepository;
    }

    public Task<IReadOnlyCollection<Vendor>> GetAllVendors()
    {
        return _repository.GetAllVendors();
    }

    public Task<Vendor?> GetVendorById(ObjectId id)
    {
        return _repository.GetVendorById(id);
    }

    public Task<Vendor> AddVendor(string name)
    {
        var vendor = new Vendor
        {
            Name = name
        };
        return _repository.AddVendor(vendor);
    }

    public Task DeleteVendor(ObjectId id)
    {
        _placeRepository.UnreservePlaceForVendor(id);
        return _repository.DeleteVendor(id);
    }

    public Task<bool> AddEmployeeToVendor(string firstName, string lastName, ObjectId vendorId)
    {
        var employee = new Employee
        {
            FirstName = firstName,
            LastName = lastName
        };
        return _repository.AddEmployeeToVendor(employee, vendorId);
    }

    public Task<List<Employee>> GetAllEmployees()
    {
        return _repository.GetAllEmployees();
    }

    public Task<bool> AddProductToVendor(ObjectId productId, ObjectId vendorId)
    {
        return _repository.AddProductToVendor(productId, vendorId);
    }

    public Task<bool> DeleteProductFromVendor(ObjectId productId)
    {
        return _repository.DeleteProductFromVendor(productId);
    }
}
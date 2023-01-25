using MongoDB.Bson;

namespace MongoDBDemoApp.Core.Workloads.Vendors;

public sealed class VendorService: IVendorService
{
    private readonly IVendorRepository _repository;
    
    public VendorService(IVendorRepository repository)
    {
        _repository = repository;
    }
    
    public Task<IReadOnlyCollection<Vendor>> GetAllVendors()
    {
        return _repository.GetAllVendors();
    }

    public Task<Vendor?> GetVendorById(ObjectId id)
    {
        return _repository.GetVendorById(id);
    }

    public Task<Vendor> AddVendor(string name, ObjectId placeId)
    {
        var vendor = new Vendor()
        {
            Name = name,
            PlaceId = placeId
        };
        return _repository.AddVendor(vendor);
    }

    public Task DeleteVendor(ObjectId id)
    {
        return _repository.DeleteVendor(id);
    }
}
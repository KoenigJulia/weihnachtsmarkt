using MongoDB.Bson;

namespace MongoDBDemoApp.Core.Workloads.Places;

public class PlaceService: IPlaceService
{
    private readonly IPlaceRepository _repository;

    public PlaceService(IPlaceRepository repository)
    {
        _repository = repository;
    }
    
    public Task<IReadOnlyCollection<Place>> GetAllPlaces()
    {
        return _repository.GetAllPlaces();
    }

    public Task<Place?> GetPlaceById(ObjectId id)
    {
        return _repository.GetPlaceById(id);
    }

    public Task<Place> AddPlace(int placeNr)
    {
        Place place = new Place()
        {
            PlaceNr = placeNr
        };
        return _repository.AddPlace(place);
    }

    public Task DeletePlace(ObjectId id)
    {
        return _repository.DeletePlace(id);
    }

    public async Task<bool> ReservePlace(ObjectId vendorId, ObjectId placeId)
    {
        Place? p = await GetPlaceById(placeId);
        if (p == null)
        {
            throw new ArgumentNullException();
        }

        if (p.VendorId != null)
        {
            throw new ArgumentException("Place is already reserved!");
        }
        
        return await _repository.ReservePlace(vendorId, placeId);
    }

    public Task<List<Place>> GetFreePlaces()
    {
        return _repository.GetFreePlaces();
    }
}
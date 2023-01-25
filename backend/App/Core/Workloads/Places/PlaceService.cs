using MongoDB.Bson;

namespace MongoDBDemoApp.Core.Workloads.Places;

public class PlaceService: IPlaceService
{
    private readonly IPlaceRepository _repository;

    public PlaceService(IPlaceRepository placeRepository, IPlaceRepository repository)
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
}
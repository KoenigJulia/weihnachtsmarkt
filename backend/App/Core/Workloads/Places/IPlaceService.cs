using MongoDB.Bson;

namespace MongoDBDemoApp.Core.Workloads.Places;

public interface IPlaceService
{
    Task<IReadOnlyCollection<Place>> GetAllPlaces();
    Task<Place?> GetPlaceById(ObjectId id);
    Task<Place> AddPlace(int placeNr);
    Task DeletePlace(ObjectId id);
    Task<bool> ReservePlace(ObjectId vendorId, ObjectId placeId);
}
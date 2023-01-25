using LeoMongo.Database;
using MongoDB.Bson;

namespace MongoDBDemoApp.Core.Workloads.Places;

public interface IPlaceRepository: IRepositoryBase
{
    Task<Place> AddPlace(Place place);
    Task<IReadOnlyCollection<Place>> GetAllPlaces();
    Task<Place?> GetPlaceById(ObjectId id);
    Task DeletePlace(ObjectId id);
    Task<bool> ReservePlace(ObjectId vendorId, ObjectId placeId);
    Task<List<Place>> GetFreePlaces();
}
using LeoMongo;
using LeoMongo.Database;
using LeoMongo.Transaction;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace MongoDBDemoApp.Core.Workloads.Places;

public class PlaceRepository: RepositoryBase<Place>, IPlaceRepository
{
    public PlaceRepository(ITransactionProvider transactionProvider, IDatabaseProvider databaseProvider) : base(transactionProvider, databaseProvider)
    {
    }

    public override string CollectionName { get; } = MongoUtil.GetCollectionName<Place>();
    

    public Task<Place> AddPlace(Place place)
    {
        return InsertOneAsync(place);
    }

    public async Task<IReadOnlyCollection<Place>> GetAllPlaces()
    {
        return await Query().ToListAsync();
    }

    public async Task<Place?> GetPlaceById(ObjectId id)
    {
        return await Query().SingleOrDefaultAsync(p => p.Id == id);
    }

    public Task DeletePlace(ObjectId id)
    {
        return DeleteOneAsync(id);
    }
}
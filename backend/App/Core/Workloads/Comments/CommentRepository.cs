using LeoMongo;
using LeoMongo.Database;
using LeoMongo.Transaction;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace MongoDBDemoApp.Core.Workloads.Comments;

public sealed class CommentRepository : RepositoryBase<Comment>, ICommentRepository
{
    public CommentRepository(ITransactionProvider transactionProvider, IDatabaseProvider databaseProvider) : base(
        transactionProvider, databaseProvider)
    {
    }

    public override string CollectionName { get; } = MongoUtil.GetCollectionName<Comment>();

    public async Task<IReadOnlyCollection<Comment>> GetCommentsForPost(ObjectId postId)
    {
        return await Query()
            .Where(x => x.PostId == postId)
            .ToListAsync();
    }

    public async Task<IReadOnlyCollection<Comment>> GetUnapprovedComments()
    {
        // TODO
        return await Query().Where(x => x.Approved == false).ToListAsync();
    }

    public async Task<bool> ApproveComment(ObjectId id)
    {
        // TODO
        // hint: UpdateDefBuilder
        var x = UpdateDefBuilder.Set(x => x.Approved, true);
        var res = await UpdateOneAsync(id, x);
        return res is { IsAcknowledged: true, ModifiedCount: 1 };
    }

    public async Task<bool> DeleteComment(ObjectId id)
    {
        // TODO
        var deleteResult = await DeleteOneAsync(id);
        return deleteResult.DeletedCount == 1;
    }

    public Task<Comment> AddComment(Comment comment)
    {
        return InsertOneAsync(comment);
    }

    public async Task<Comment?> GetCommentById(ObjectId id)
    {
        return await Query()
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync();
    }

    public async Task DeleteCommentsByPost(ObjectId postId)
    {
        await DeleteManyAsync(x => x.PostId == postId);
    }
}
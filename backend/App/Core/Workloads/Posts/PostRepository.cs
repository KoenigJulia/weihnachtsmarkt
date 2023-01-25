using LeoMongo;
using LeoMongo.Database;
using LeoMongo.Transaction;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MongoDBDemoApp.Core.Util;
using MongoDBDemoApp.Core.Workloads.Comments;

namespace MongoDBDemoApp.Core.Workloads.Posts;

public sealed class PostRepository : RepositoryBase<Post>, IPostRepository
{
    private readonly ICommentRepository _commentRepository;

    public PostRepository(ITransactionProvider transactionProvider, IDatabaseProvider databaseProvider,
        ICommentRepository commentRepository) : base(
        transactionProvider, databaseProvider)
    {
        _commentRepository = commentRepository;
    }

    public override string CollectionName { get; } = MongoUtil.GetCollectionName<Post>();

    public async Task<IReadOnlyCollection<Post>> GetAllPosts()
    {
        return await Query().ToListAsync();
    }

    public async Task<Post?> GetPostById(ObjectId id)
    {
        // TODO
        return await Query().SingleOrDefaultAsync(x => x.Id == id);
    }

    public Task<Post> AddPost(Post post)
    {
        // TODO
        return InsertOneAsync(post);
    }

    public Task DeletePost(ObjectId id)
    {
        // TODO
        return DeleteOneAsync(id);
    }

    public async Task<(ObjectId PostId, List<ObjectId>? CommentIds)?> GetPostWithComments(ObjectId postId)
    {
        IDictionary<ObjectId, List<ObjectId>?> postsWithComments =
            await QueryIncludeDetail<Comment>(_commentRepository,(comment => (comment.PostId)) , x => x.Id == postId).ToDictionaryAsync();
            
        if (postsWithComments.Count == 0
            || postsWithComments.Keys.All(p => p != postId))
        {
            return null;
        }

        return postsWithComments.First().ToTuple();
    }
}
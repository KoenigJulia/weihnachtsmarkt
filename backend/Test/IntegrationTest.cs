using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using LeoMongo;
using LeoMongo.Database;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDBDemoApp.Core.Util;
using MongoDBDemoApp.Core.Workloads.Comments;
using MongoDBDemoApp.Core.Workloads.Posts;
using Xunit;

namespace MongoDBDemoApp.Test;

public sealed class IntegrationTest : IDisposable
{
    private IDatabaseProvider DatabaseProvider { get; set; }

    private IMongoConfig MongoConfig { get; set; }
    
    private IConfiguration Configuration { get; }
    
    private AppSettings? MyAppSettings { get; set; }

    private IOptions<AppSettings> MyOptions { get; set; }

    public IntegrationTest()
    {
        MyAppSettings = new AppSettings()
        {
            ConnectionString = "mongodb://localhost",
            Database = "test_blog"
        };
        MyOptions = Options.Create(MyAppSettings);
        MongoConfig = new MongoConfig(MyOptions);
        DatabaseProvider = new DatabaseProvider(MongoConfig);
    }

    [Fact]
    public async Task TestApproveComment()
    {
        var dateTimeProvider = new DateTimeProvider();
        var transactionProvider = new TransactionProvider(DatabaseProvider);

        var commentRepository = new CommentRepository(transactionProvider, DatabaseProvider);
        var commentService = new CommentService(dateTimeProvider, commentRepository);

        var postRepository = new PostRepository(transactionProvider, DatabaseProvider, commentRepository);
        var postService = new PostService(
            dateTimeProvider,
            postRepository,
            commentRepository,
            new Logger<PostService>(new LoggerFactory())
        );

        var post = await postService.AddPost("All I want for christmas", "Der kleine Tim", "Is youu!");
        

        var comment = await commentService.AddComment(
            post,
            "ChristKind",
            "christkind@weihnachten.at",
            "Frohe Weihnachten!"
        );

        bool isApproved = comment.Approved;
        
        isApproved.Should().BeFalse();
        isApproved = await commentService.ApproveComment(comment);
        isApproved.Should().BeTrue();
        
        var comment2 = await commentService.AddComment(
            post,
            "Niño Jesús",
            "nino-jesus@navidad.es",
            "Feliz Navidad!"
        );

        var unapprovedComments = await commentService.GetUnapprovedComments();
        unapprovedComments.Count.Should().Be(1);
        unapprovedComments.ToList()[0].Name.Should().Be("Niño Jesús");
        
        // because of special characters, the comment is being declined
        var deleteCommentResult = await commentService.DeleteComment(comment2);
        deleteCommentResult.Should().BeTrue();
        unapprovedComments = await commentService.GetUnapprovedComments();
        
        unapprovedComments.Count.Should().Be(0);
        
        await postService.DeletePost(post.Id);

        var posts = await postService.GetAllPosts();
        posts.Count.Should().Be(0);

        var comments = await commentService.GetCommentsForPost(post);
        comments.Count().Should().Be(0);
        
        // add one final post to test the Dispose method which deletes the database after testing
        post = await postService.AddPost("Houston, we've had a problem here", "Jack Swigert",
            "The dispose method is not working!");
        post.Id.ToString().Should().MatchRegex("^[0-9a-z]+$");
    }

    public void Dispose()
    {
        var client = new MongoClient(MyAppSettings.ConnectionString);
        client.DropDatabase(MyAppSettings.Database);
    }
}
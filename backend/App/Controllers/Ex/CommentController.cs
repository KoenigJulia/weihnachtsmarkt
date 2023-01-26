using Microsoft.AspNetCore.Mvc;


namespace MongoDBDemoApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public sealed class CommentController : ControllerBase
{
    /*private readonly ILogger<CommentController> _logger;
    private readonly IMapper _mapper;
    private readonly IPostService _postService;
    private readonly ICommentService _service;
    private readonly ITransactionProvider _transactionProvider;

    public CommentController(IMapper mapper, ICommentService service, ITransactionProvider transactionProvider,
        IPostService postService, ILogger<CommentController> logger)
    {
        _mapper = mapper;
        _service = service;
        _transactionProvider = transactionProvider;
        _postService = postService;
        _logger = logger;
    }

    /// <summary>
    ///     Returns all comments for the post with the given id.
    /// </summary>
    /// <param name="postId">id of an existing post</param>
    /// <returns>List of comments, may be empty</returns>
    [HttpGet]
    [Route("post")]
    public async Task<ActionResult<IReadOnlyCollection<CommentDto>>> GetCommentsForPost(string postId)
    {
        Post? post;
        if (string.IsNullOrWhiteSpace(postId) ||
            (post = await _postService.GetPostById(new ObjectId(postId))) == null)
        {
            return BadRequest();
        }

        IReadOnlyCollection<Comment> comments = await _service.GetCommentsForPost(post);
        return Ok(_mapper.Map<List<CommentDto>>(comments));
    }

    /// <summary>
    ///     Returns all unapproved comments.
    /// </summary>
    /// <returns>List of unapproved comments, may be empty</returns>
    [HttpGet]
    [Route("unapproved")]
    public async Task<ActionResult<IReadOnlyCollection<CommentDto>>> GetUnapprovedComments()
    {
        IReadOnlyCollection<Comment> comments = await _service.GetUnapprovedComments();
        return Ok(_mapper.Map<List<CommentDto>>(comments));
    }

    /// <summary>
    ///     Sets an existing comment as approved.
    /// </summary>
    /// <param name="id">The id of the comment</param>
    [HttpPut]
    [Route("approve")]
    public async Task<IActionResult> ApproveComment([FromBody] ApproveCommentRequest request)
    {
        string? id = request?.Id;
        Comment? comment;
        if (string.IsNullOrWhiteSpace(id)
            || (comment = await _service.GetCommentById(new ObjectId(id))) == null)
        {
            return BadRequest();
        }

        if (!await _service.ApproveComment(comment))
        {
            _logger.LogInformation($"Duplicate approval of comment {id}");
        }

        return Ok();
    }

    /// <summary>
    ///     Deletes a comment.
    /// </summary>
    /// <param name="id">The id of the comment</param>
    [HttpDelete]
    public async Task<IActionResult> DeleteComment(string id)
    {
        var comment = await _service.GetCommentById(new (id));
        if (comment == null)
        {
            return BadRequest();
        }

        return Ok(await _service.DeleteComment(comment));
    }

    /// <summary>
    ///     Returns the comment identified by the given id if it exists.
    /// </summary>
    /// <param name="id">existing comment id</param>
    /// <returns>a comment</returns>
    [HttpGet]
    public async Task<ActionResult<CommentDto>> GetById(string id)
    {
        Comment? comment;
        if (string.IsNullOrWhiteSpace(id)
            || (comment = await _service.GetCommentById(new ObjectId(id))) == null)
        {
            return BadRequest();
        }

        return Ok(_mapper.Map<CommentDto>(comment));
    }

    /// <summary>
    ///     Creates a new comment.
    /// </summary>
    /// <param name="request">Data for the new comment</param>
    /// <returns>the created comment if successful</returns>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCommentRequest request)
    {
        Comment comment = await _service.AddComment(
            new Post() { Id = new(request.PostId) },
            request.Name,
            request.Mail,
            request.Text
        );
        return Ok(comment);
    }*/
}
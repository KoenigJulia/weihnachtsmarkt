using AutoMapper;
using LeoMongo.Transaction;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using MongoDB.Bson;
using MongoDBDemoApp.Core.Workloads.Orders;
using MongoDBDemoApp.Model.Order;
using MongoDBDemoApp.Model.Product;

namespace MongoDBDemoApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;
    private readonly ITransactionProvider _transactionProvider;
    private readonly IMapper _mapper;

    public OrderController(ILogger<CommentController> logger,
        IOrderService orderService,
        ITransactionProvider transactionProvider,
        IMapper mapper)
    {
        _orderService = orderService;
        _transactionProvider = transactionProvider;
        _mapper = mapper;
    }

    [HttpGet]
    [Route("order")]
    public async Task<IActionResult> GetById(string orderId)
    {
        Order? order;

        if (string.IsNullOrWhiteSpace(orderId) ||
            (order = await _orderService.GetOrderById(new ObjectId(orderId))) == null)
        {
            return BadRequest();
        }

        return Ok(_mapper.Map<OrderDto>(order));
    }

    [HttpGet]
    [Route("all")]
    public async Task<ActionResult<IReadOnlyCollection<Order>>> GetAll()
    {
        var orders = await _orderService.GetOrders();
        return Ok(_mapper.Map<IReadOnlyCollection<OrderDto>>(orders));
    }
    
    [HttpGet]
    [Route("price")]
    public async Task<ActionResult<IReadOnlyCollection<Order>>> GetPrice(string orderId)
    {
        return Ok(await _orderService.GetOrderPrice(new ObjectId(orderId)));
    }

    [HttpPost]
    [Route("order")]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.CustomerId))
        {
            return BadRequest();
        }

        using var transaction = await _transactionProvider.BeginTransaction();
        var newOrder = new Order()
        {
            Name = request.Name,
            CustomerId = new ObjectId(request.CustomerId),
            Created = DateTime.Now,
        };
        request.Products.ForEach(oi => { newOrder.Products.Add(new ObjectId(oi)); });

        var order = await _orderService.AddOrder(newOrder);

        await transaction.CommitAsync();
        return CreatedAtAction(nameof(GetById), new {id = order.Id.ToString()}, _mapper.Map<OrderDto>(order));
    }

    [HttpDelete]
    [Route("order")]
    public async Task<IActionResult> DeleteOrder(string orderId)
    {
        var customer = await _orderService.GetOrderById(new ObjectId(orderId));
        if (customer == null)
        {
            return BadRequest();
        }

        return Ok(await _orderService.DeleteOrder(new ObjectId(orderId)));
    }

    [HttpPost]
    [Route("order/{orderId}/product")]
    public async Task<IActionResult> AddProductToOrder(string orderId,
        [FromBody] AddProductRequest request)
    {
        if (string.IsNullOrWhiteSpace(orderId) || string.IsNullOrWhiteSpace(orderId))
        {
            return BadRequest();
        }

        using var transaction = await _transactionProvider.BeginTransaction();
        await _orderService.AddProduct(new ObjectId(orderId), new ObjectId(request.ProductId));
        await transaction.CommitAsync();

        var order = await _orderService.GetOrderById(new ObjectId(orderId));
        return Ok(_mapper.Map<OrderDto>(order));
    }

    [HttpDelete]
    [Route("order/{orderId}/product/{productId}")]
    public async Task<IActionResult> RemoveProductFromOrder(string orderId,
        string productId)
    {
        if (string.IsNullOrWhiteSpace(orderId) || string.IsNullOrWhiteSpace(orderId))
        {
            return BadRequest();
        }

        using var transaction = await _transactionProvider.BeginTransaction();
        await _orderService.DeleteProductOfOrder(new ObjectId(orderId), new ObjectId(productId));
        await transaction.CommitAsync();

        var order = await _orderService.GetOrderById(new ObjectId(orderId));
        return Ok(_mapper.Map<OrderDto>(order));
    }

    [HttpGet]
    [Route("order/{orderId}/products")]
    public async Task<IActionResult> GetProductsOfOrder(string orderId)
    {
        if (string.IsNullOrWhiteSpace(orderId))
        {
            return BadRequest();
        }

        var products = await _orderService.GetProductsForOrder(new ObjectId(orderId));
        return Ok(_mapper.Map<IReadOnlyCollection<ProductDto>>(products));
    }
}
using LeoMongo.Transaction;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using MongoDB.Bson;
using MongoDBDemoApp.Core.Workloads.Orders;
using MongoDBDemoApp.Model.Order;

namespace MongoDBDemoApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController: ControllerBase
{
    
    private readonly IOrderService _orderService;
    private readonly ITransactionProvider _transactionProvider;
    
    public OrderController(ILogger<CommentController> logger,
        IOrderService orderService,
        ITransactionProvider transactionProvider)
    {
        _orderService = orderService;
        _transactionProvider = transactionProvider;
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

        return Ok(order);
    }

    [HttpGet]
    [Route("all")]
    public async Task<ActionResult<IReadOnlyCollection<Order>>> GetAll()
    {
        var orders = await _orderService.GetOrders();
        return Ok(orders);
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
        var order = await _orderService.AddOrder(new Order()
        {
            CustomerId = new ObjectId(request.CustomerId)
        });
        request.OrderItems.ForEach(oi => { order.OrderItems.Add(new ObjectId(oi)); });

        await transaction.CommitAsync();
        return CreatedAtAction(nameof(GetById), new {id = order.Id.ToString()}, order);
    }
}
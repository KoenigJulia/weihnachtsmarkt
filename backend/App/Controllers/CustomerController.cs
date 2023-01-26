using AutoMapper;
using LeoMongo.Transaction;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDBDemoApp.Core.Workloads.Customers;
using MongoDBDemoApp.Model.CreateCustomerRequest;

namespace MongoDBDemoApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public sealed class CustomerController : ControllerBase
{
    private readonly ICustomerService _customerService;
    private readonly ITransactionProvider _transactionProvider;
    private readonly IMapper _mapper;


    public CustomerController(ILogger<CommentController> logger,
        IMapper mapper,
        ICustomerService customerService,
        ITransactionProvider transactionProvider)
    {
        _mapper = mapper;
        _customerService = customerService;
        _transactionProvider = transactionProvider;
    }

    [HttpGet]
    [Route("customer")]
    public async Task<IActionResult> GetById(string customerId)
    {
        Customer? customer;

        if (string.IsNullOrWhiteSpace(customerId) ||
            (customer = await _customerService.GetCustomerById(new ObjectId(customerId))) == null)
        {
            return BadRequest();
        }

        return Ok(_mapper.Map<CustomerDto>(customer));
    }

    [HttpGet]
    [Route("all")]
    public async Task<ActionResult<IReadOnlyCollection<Customer>>> GetAll()
    {
        var customers = await _customerService.GetCustomers();
        return Ok(_mapper.Map<IReadOnlyCollection<CustomerDto>>(customers));
    }

    [HttpPost]
    [Route("customer")]
    public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.FirstName)
            || string.IsNullOrWhiteSpace(request.LastName)
            || string.IsNullOrWhiteSpace(request.PhoneNumber))
        {
            return BadRequest();
        }

        using var transaction = await _transactionProvider.BeginTransaction();
        var customer = await _customerService.AddCustomer(new Customer()
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            PhoneNumber = request.PhoneNumber
        });
        await transaction.CommitAsync();

        return CreatedAtAction(nameof(GetById), new {id = customer.Id.ToString()}, _mapper.Map<CustomerDto>(customer));
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteCustomer(string id)
    {
        var customer = await _customerService.GetCustomerById(new ObjectId(id));
        if (customer == null)
        {
            return BadRequest();
        }

        return Ok(await _customerService.DeleteCustomer(new ObjectId(id)));
    }
}
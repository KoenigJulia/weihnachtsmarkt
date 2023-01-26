using AutoMapper;
using LeoMongo.Transaction;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDBDemoApp.Core.Workloads.Vendors;
using MongoDBDemoApp.Model.Vendor;

namespace MongoDBDemoApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public sealed class VendorController : ControllerBase
{
    private readonly ITransactionProvider _transactionProvider;
    private readonly IVendorService _vendorService;
    private readonly IMapper _mapper;

    public VendorController(ILogger<VendorController> logger, IMapper mapper, IVendorService vendorService,
        ITransactionProvider transactionProvider)
    {
        _mapper = mapper;
        _vendorService = vendorService;
        _transactionProvider = transactionProvider;
    }

    [HttpGet]
    [Route("vendor")]
    public async Task<IActionResult> GetById(string vendorId)
    {
        Vendor? vendor;

        if (string.IsNullOrWhiteSpace(vendorId) ||
            (vendor = await _vendorService.GetVendorById(new ObjectId(vendorId))) == null)
            return BadRequest();

        return Ok(_mapper.Map<VendorDto>(vendor));
    }

    [HttpGet]
    [Route("all")]
    public async Task<ActionResult<IReadOnlyCollection<Vendor>>> GetAll()
    {
        var vendors = await _vendorService.GetAllVendors();
        return Ok(_mapper.Map<List<VendorDto>>(vendors));
    }

    [HttpPost]
    [Route("vendor")]
    public async Task<IActionResult> CreateVendor([FromBody] CreateVendorRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
            return BadRequest();

        using var transaction = await _transactionProvider.BeginTransaction();
        var vendor = await _vendorService.AddVendor(request.Name);
        await transaction.CommitAsync();

        return CreatedAtAction(nameof(GetById), new { id = vendor.Id.ToString() }, _mapper.Map<VendorDto>(vendor));
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteVendor(string id)
    {
        var vendor = await _vendorService.GetVendorById(new ObjectId(id));
        if (vendor == null) return BadRequest();
        await _vendorService.DeleteVendor(new ObjectId(id));

        return Ok(true);
    }
}
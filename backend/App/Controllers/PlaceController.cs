using AutoMapper;
using LeoMongo.Transaction;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDBDemoApp.Core.Workloads.Places;
using MongoDBDemoApp.Model.Place;

namespace MongoDBDemoApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public sealed class PlaceController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IPlaceService _placeService;
    private readonly ITransactionProvider _transactionProvider;

    public PlaceController(ILogger<PlaceController> logger, IMapper mapper, IPlaceService placeService,
        ITransactionProvider transactionProvider)
    {
        _placeService = placeService;
        _transactionProvider = transactionProvider;
        _mapper = mapper;
    }


    [HttpGet]
    [Route("place")]
    public async Task<IActionResult> GetById(string placeId)
    {
        Place? place;

        if (string.IsNullOrWhiteSpace(placeId) ||
            (place = await _placeService.GetPlaceById(new ObjectId(placeId))) == null)
            return BadRequest();

        return Ok(_mapper.Map<PlaceDto>(place));
    }

    [HttpGet]
    [Route("all")]
    public async Task<ActionResult<IReadOnlyCollection<Place>>> GetAll()
    {
        var places = await _placeService.GetAllPlaces();
        return Ok(_mapper.Map<List<PlaceDto>>(places));
    }

    [HttpPost]
    [Route("place")]
    public async Task<IActionResult> CreatePlace([FromBody] CreatePlaceRequest request)
    {
        using var transaction = await _transactionProvider.BeginTransaction();
        var place = await _placeService.AddPlace(request.PlaceNr);
        await transaction.CommitAsync();

        return CreatedAtAction(nameof(GetById), new { id = place.Id.ToString() }, _mapper.Map<PlaceDto>(place));
    }

    [HttpDelete]
    public async Task<IActionResult> DeletePlace(string id)
    {
        var place = await _placeService.GetPlaceById(new ObjectId(id));
        if (place == null) return BadRequest();
        await _placeService.DeletePlace(new ObjectId(id));
        return Ok(true);
    }

    [HttpPost]
    [Route("reserve")]
    public async Task<IActionResult> ReservePlace([FromBody] ReservePlaceRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.VendorId) || string.IsNullOrWhiteSpace(request.PlaceId))
            return BadRequest();

        var res = await _placeService.ReservePlace(new ObjectId(request.VendorId), new ObjectId(request.PlaceId));
        return Ok(res);
    }
}
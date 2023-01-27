namespace MongoDBDemoApp.Model.Place;

public class ReservePlaceRequest
{
    public string VendorId { get; set; } = default!;
    public string PlaceId { get; set; } = default!;
}
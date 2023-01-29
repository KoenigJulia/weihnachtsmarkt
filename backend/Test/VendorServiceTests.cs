using System.Threading.Tasks;
using FluentAssertions;
using MongoDB.Bson;
using MongoDBDemoApp.Core.Workloads.Places;
using MongoDBDemoApp.Core.Workloads.Vendors;
using NSubstitute;
using Xunit;

namespace MongoDBDemoApp.Test;

public class VendorServiceTests
{
    [Fact]
    public async Task TestAddProductToVendor()
    {
        var vendorId = new ObjectId();
        var productId = new ObjectId();

        var repoMock = Substitute.For<IVendorRepository>();
        repoMock.AddProductToVendor(Arg.Is(productId), Arg.Is(vendorId)).Returns(true);

        var service = new VendorService(repoMock, Substitute.For<IPlaceRepository>());
        var result = await service.AddProductToVendor(productId, vendorId);

        await repoMock.Received(1).AddProductToVendor(Arg.Is(productId), Arg.Is(vendorId));
        result.Should().BeTrue();
    }
}
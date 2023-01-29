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

    [Fact]
    public async Task TestDeleteProductFromVendor()
    {
        var productId = new ObjectId();

        var repoMock = Substitute.For<IVendorRepository>();
        repoMock.DeleteProductFromVendor(Arg.Is(productId)).Returns(true);

        var service = new VendorService(repoMock, Substitute.For<IPlaceRepository>());
        var result = await service.DeleteProductFromVendor(productId);

        await repoMock.Received(1).DeleteProductFromVendor(Arg.Is(productId));
        result.Should().BeTrue();
    }

    [Fact]
    public async Task TestAddEmployeeToVendor()
    {
        var employee = new Employee
        {
            FirstName = "FirstTest",
            LastName = "LastTest"
        };
        var vendorId = new ObjectId();


        var repoMock = Substitute.For<IVendorRepository>();
        repoMock.AddEmployeeToVendor(Arg.Is(employee), Arg.Is(vendorId)).Returns(true);

        var service = new VendorService(repoMock, Substitute.For<IPlaceRepository>());
        var result = await service.AddEmployeeToVendor(employee, vendorId);

        await repoMock.Received(1).AddEmployeeToVendor(Arg.Is(employee), Arg.Is(vendorId));
        result.Should().BeTrue();
    }
}
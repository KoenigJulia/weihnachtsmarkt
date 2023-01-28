using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using MongoDB.Bson;
using MongoDBDemoApp.Core.Util;
using MongoDBDemoApp.Core.Workloads.Orders;
using MongoDBDemoApp.Core.Workloads.Products;
using NSubstitute;
using Xunit;

namespace MongoDBDemoApp.Test;

public sealed class OrderServiceTests
{
    [Fact]
    public async Task TestGetProductsForOrder()
    {
        var orderId = new ObjectId();
        Product[] expectedProducts =
        {
            new Product
            {
                Id = new ObjectId(),
                Name = "Apple Punch",
                Price = 1.99f,
            },
            new Product
            {
                Id = new ObjectId(),
                Name = "Orange Punch",
                Price = 2.50f,
            }
        };
        
        var repoMock = Substitute.For<IOrderRepository>();
        repoMock.GetProductsForOrder(Arg.Is(orderId)).Returns(expectedProducts);

        var service = new OrderService(Substitute.For<IDateTimeProvider>(), repoMock);
        IReadOnlyCollection<Product> products = await service.GetProductsForOrder(orderId);

        await repoMock.Received(1).GetProductsForOrder(Arg.Is(orderId));
        products.Should().NotBeNullOrEmpty()
            .And.HaveCount(2)
            .And.Contain(expectedProducts);
    }
}
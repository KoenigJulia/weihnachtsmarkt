using System.Collections.Generic;
using System.Linq;
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
        repoMock.GetProductsForOrder(Arg.Is(orderId))
            .Returns(expectedProducts);

        var service = new OrderService(Substitute.For<IDateTimeProvider>(), repoMock);
        IReadOnlyCollection<Product> products = await service.GetProductsForOrder(orderId);

        await repoMock.Received(1)
            .GetProductsForOrder(Arg.Is(orderId));
        products.Should()
            .NotBeNullOrEmpty()
            .And.HaveCount(2)
            .And.Contain(expectedProducts);
    }

    [Fact]
    public async Task TestGetProductsForOrder_WithEmptyProducts()
    {
        var orderId = new ObjectId();
        Product[] expectedProducts = { };

        var repoMock = Substitute.For<IOrderRepository>();
        repoMock.GetProductsForOrder(Arg.Is(orderId))
            .Returns(expectedProducts);

        var service = new OrderService(Substitute.For<IDateTimeProvider>(), repoMock);
        IReadOnlyCollection<Product> products = await service.GetProductsForOrder(orderId);

        await repoMock.Received(1)
            .GetProductsForOrder(Arg.Is(orderId));
        products.Should()
            .BeEmpty();
    }

    [Fact]
    public async Task TestAddProductToOrder()
    {
        var orderId = new ObjectId();
        var productId = new ObjectId();

        var repoMock = Substitute.For<IOrderRepository>();
        repoMock.AddProduct(Arg.Is(orderId), Arg.Is(productId))
            .Returns(true);

        var service = new OrderService(Substitute.For<IDateTimeProvider>(), repoMock);
        var result = await service.AddProduct(orderId, productId);
        
        repoMock.GetOrderById(Arg.Is(orderId))
            .Returns(new Order
            {
                Id = orderId,
                Products = new List<ObjectId>
                {
                    productId
                }
            });

        await repoMock.Received(1)
            .AddProduct(Arg.Is(orderId), Arg.Is(productId));
        result.Should()
            .BeTrue();

        var order = await repoMock.GetOrderById(orderId);
        order.Should()
            .NotBeNull();
        order?.Products.Should()
            .NotBeNullOrEmpty()
            .And.HaveCount(1)
            .And.ContainSingle(p => p == productId);
    }

    [Fact]
    public async Task TestDeleteProductFromOrder()
    {
        var orderId = new ObjectId();
        var productId = new ObjectId();

        var repoMock = Substitute.For<IOrderRepository>();
        repoMock.DeleteProductOfOrder(Arg.Is(orderId), Arg.Is(productId))
            .Returns(true);

        var service = new OrderService(Substitute.For<IDateTimeProvider>(), repoMock);
        var result = await service.DeleteProductOfOrder(orderId, productId);

        await repoMock.Received(1)
            .DeleteProductOfOrder(Arg.Is(orderId), Arg.Is(productId));
        result.Should()
            .BeTrue();
    }

    [Fact]
    public async Task TestDeleteProductsFromOrder()
    {
        var orderId = new ObjectId();

        var repoMock = Substitute.For<IOrderRepository>();
        repoMock.DeleteProductsOfOrder(Arg.Is(orderId))
            .Returns(true);

        var service = new OrderService(Substitute.For<IDateTimeProvider>(), repoMock);
        var result = await service.DeleteProductsOfOrder(orderId);

        await repoMock.Received(1)
            .DeleteProductsOfOrder(Arg.Is(orderId));
        result.Should()
            .BeTrue();
    }

    [Fact]
    public async Task TestGetPrice()
    {
        var orderId = new ObjectId();
        var products = new List<Product>
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

        var expectedPrice = products.Sum(p => p.Price);

        var repoMock = Substitute.For<IOrderRepository>();
        repoMock.GetOrderPrice(Arg.Is(orderId))
            .Returns(expectedPrice);

        var service = new OrderService(Substitute.For<IDateTimeProvider>(), repoMock);
        var price = await service.GetOrderPrice(orderId);

        await repoMock.Received(1)
            .GetOrderPrice(Arg.Is(orderId));
        price.Should()
            .Be(expectedPrice);
    }
}
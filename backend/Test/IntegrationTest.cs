using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LeoMongo;
using LeoMongo.Database;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDBDemoApp.Core.Util;
using MongoDBDemoApp.Core.Workloads.Customers;
using MongoDBDemoApp.Core.Workloads.Orders;
using MongoDBDemoApp.Core.Workloads.Places;
using MongoDBDemoApp.Core.Workloads.Products;
using MongoDBDemoApp.Core.Workloads.Vendors;
using Xunit;

namespace MongoDBDemoApp.Test;

public sealed class IntegrationTest : IDisposable
{
    private IDatabaseProvider DatabaseProvider { get; set; }

    private IMongoConfig MongoConfig { get; set; }

    private AppSettings? MyAppSettings { get; set; }

    private IOptions<AppSettings> MyOptions { get; set; }

    public IntegrationTest()
    {
        MyAppSettings = new AppSettings()
        {
            ConnectionString = "mongodb://localhost",
            Database = "test_db"
        };
        MyOptions = Options.Create(MyAppSettings);
        MongoConfig = new MongoConfig(MyOptions);
        DatabaseProvider = new DatabaseProvider(MongoConfig);
    }

    [Fact]
    public async Task TestCreateOrder()
    {
        var dateTimeProvider = new DateTimeProvider();
        var transactionProvider = new TransactionProvider(DatabaseProvider);
        
        var vendorRepository = new VendorRepository(transactionProvider, DatabaseProvider);
        var placeRepository = new PlaceRepository(transactionProvider, DatabaseProvider);
        var customerRepository = new CustomerRepository(transactionProvider, DatabaseProvider);
        var productRepository = new ProductRepository(transactionProvider, DatabaseProvider);
        var orderRepository = new OrderRepository(transactionProvider, DatabaseProvider, productRepository);
        
        var vendorService = new VendorService(vendorRepository, placeRepository);
        var placeService = new PlaceService(placeRepository);
        var customerService = new CustomerService(dateTimeProvider, customerRepository);
        var productService = new ProductService(productRepository, vendorRepository);
        var orderService = new OrderService(dateTimeProvider, orderRepository);

        var vendor = await vendorService.AddVendor("Filly's Punch");
        
        Assert.NotNull(vendor);
        Assert.NotNull(vendor.Id);
        Assert.Equal("Filly's Punch", vendor.Name);
        
        var place = await placeService.AddPlace(1);
        
        Assert.NotNull(place);
        Assert.NotNull(place.Id);
        Assert.Equal(1, place.PlaceNr);
        
        
        var res =await placeService.ReservePlace(vendor.Id, place.Id);
        place = await placeService.GetPlaceById(place.Id);

        Assert.True(res);
        Assert.Equal(vendor.Id, place.VendorId);

        var newCustomer = new Customer()
        {
            FirstName = "Tim",
            LastName = "Tom",
            PhoneNumber = "1234567890"
        };
        
        var customer = await customerService.AddCustomer(newCustomer);
        
        Assert.NotNull(customer);
        Assert.NotNull(customer.Id);
        Assert.Equal("Tim", customer.FirstName);
        Assert.Equal("Tom", customer.LastName);
        Assert.Equal("1234567890", customer.PhoneNumber);

        var product = await productService.AddProduct("Apple Punch", 2.20f, vendor.Id);
        
        Assert.NotNull(product);
        Assert.NotNull(product.Id);
        Assert.Equal("Apple Punch", product.Name);
        Assert.Equal(2.20f, product.Price);

        var newOrder = new Order()
        {
            Name = "Order 1",
            CustomerId = customer.Id,
            Products = new List<ObjectId>()
            {
                product.Id
            }
        };
        
        var order = await orderService.AddOrder(newOrder);
        
        Assert.NotNull(order);
        Assert.NotNull(order.Id);
        Assert.Equal("Order 1", order.Name);
        Assert.Equal(customer.Id, order.CustomerId);
        Assert.Single(order.Products);
        Assert.Equal(product.Id, order.Products[0]);
    }

    public void Dispose()
    {
        var client = new MongoClient(MyAppSettings.ConnectionString);
        client.DropDatabase(MyAppSettings.Database);
    }
}
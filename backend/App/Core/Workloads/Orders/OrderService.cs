using LeoMongo;
using MongoDB.Bson;
using MongoDBDemoApp.Core.Util;
using MongoDBDemoApp.Core.Workloads.Products;

namespace MongoDBDemoApp.Core.Workloads.Orders;

public sealed class OrderService : IOrderService
{
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IOrderRepository _repository;

    public OrderService(IDateTimeProvider dateTimeProvider, IOrderRepository repository)
    {
        _dateTimeProvider = dateTimeProvider;
        _repository = repository;
    }
    
    public string CollectionName { get; } = MongoUtil.GetCollectionName<Order>();

    public async Task<Order> AddOrder(Order order)
    {
        return await _repository.AddOrder(order);
    }

    public async Task<IReadOnlyCollection<Order>> GetOrders()
    {
        return await _repository.GetOrders();
    }

    public async Task<Order?> GetOrderById(ObjectId id)
    {
        return await _repository.GetOrderById(id);
    }

    public async Task<bool> DeleteOrder(ObjectId id)
    {
        return await _repository.DeleteOrder(id);
    }

    public async Task<IReadOnlyCollection<Product>> GetProductsForOrder(ObjectId orderId)
    {
        return await _repository.GetProductsForOrder(orderId);
    }

    public async Task<bool> AddProduct(ObjectId orderId, ObjectId product)
    {
        return await _repository.AddProduct(orderId, product);
    }

    public async Task<bool> DeleteProductOfOrder(ObjectId orderId, ObjectId product)
    {
        return await _repository.DeleteProductOfOrder(orderId, product);
    }

    public async Task<bool> DeleteProductsOfOrder(ObjectId orderId)
    {
        return await _repository.DeleteProductsOfOrder(orderId);
    }

    public Task<bool> DeleteOrdersOfCustomer(ObjectId customerId)
    {
        return _repository.DeleteOrdersOfCustomer(customerId);
    }

    public Task<float> GetOrderPrice(ObjectId orderId)
    {
        return _repository.GetOrderPrice(orderId);
    }
}
using LeoMongo.Database;
using MongoDB.Bson;

namespace MongoDBDemoApp.Core.Workloads.Order;

public interface IOrderRepository : IRepositoryBase
{
    Task<Order> AddOrder(Order order);
    Task<IReadOnlyCollection<Order>> GetOrders();
    Task<Order?> GetOrderById(ObjectId id);
    Task<bool> DeleteOrder(ObjectId id);
    Task<IReadOnlyCollection<OrderItem>> GetOrderItemsForOrder(ObjectId orderId);
    Task<bool> AddOrderItem(ObjectId orderId,OrderItem orderItem);
    Task<bool> DeleteOrderItemOfOrder(ObjectId orderId,OrderItem orderItem);
    Task<bool> DeleteOrderItemsOfOrder(ObjectId orderId);
}
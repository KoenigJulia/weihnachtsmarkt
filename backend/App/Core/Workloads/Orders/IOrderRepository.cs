using LeoMongo.Database;
using MongoDB.Bson;
using MongoDBDemoApp.Core.Workloads.Products;

namespace MongoDBDemoApp.Core.Workloads.Orders;

public interface IOrderRepository : IRepositoryBase
{
    Task<Order> AddOrder(Order order);
    Task<IReadOnlyCollection<Order>> GetOrders();
    Task<Order?> GetOrderById(ObjectId id);
    Task<bool> DeleteOrder(ObjectId id);
    Task<IReadOnlyCollection<Product>> GetOrderItemsForOrder(ObjectId orderId);
    Task<bool> AddOrderItem(ObjectId orderId,Product orderItem);
    Task<bool> DeleteOrderItemOfOrder(ObjectId orderId,Product orderItem);
    Task<bool> DeleteOrderItemsOfOrder(ObjectId orderId);
}
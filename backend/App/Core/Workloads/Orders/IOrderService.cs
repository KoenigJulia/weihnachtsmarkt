using LeoMongo.Database;
using MongoDB.Bson;
using MongoDBDemoApp.Core.Workloads.Products;

namespace MongoDBDemoApp.Core.Workloads.Orders;

public interface IOrderService : IRepositoryBase
{
    Task<Order> AddOrder(Order order);
    Task<IReadOnlyCollection<Order>> GetOrders();
    Task<Order?> GetOrderById(ObjectId id);
    Task<bool> DeleteOrder(ObjectId id);
    Task<IReadOnlyCollection<Product>> GetProductsForOrder(ObjectId orderId);
    Task<bool> AddProduct(ObjectId orderId,ObjectId product);
    Task<bool> DeleteProductOfOrder(ObjectId orderId,ObjectId product);
    Task<bool> DeleteProductsOfOrder(ObjectId orderId);
}
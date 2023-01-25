using LeoMongo;
using LeoMongo.Database;
using LeoMongo.Transaction;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MongoDBDemoApp.Core.Workloads.Comments;

namespace MongoDBDemoApp.Core.Workloads.Order;

public sealed class OrderRepository : RepositoryBase<Order>, IOrderRepository
{
    public OrderRepository(ITransactionProvider transactionProvider, IDatabaseProvider databaseProvider) : base(
        transactionProvider, databaseProvider)
    {
    }

    public override string CollectionName { get; } = MongoUtil.GetCollectionName<Order>();

    

    /*public async Task<IReadOnlyCollection<Comment>> GetUnapprovedComments()
    {
        return await Query().Where(x => x.Approved == false).ToListAsync();
    }*/

    /*public async Task<bool> ApproveComment(ObjectId id)
    {
        var x = UpdateDefBuilder.Set(x => x.Approved, true);
        var res = await UpdateOneAsync(id, x);
        return res is { IsAcknowledged: true, ModifiedCount: 1 };
    }*/

    public async Task<bool> DeleteOrderItem(ObjectId id)
    {
        var deleteResult = await DeleteOneAsync(id);
        return deleteResult.DeletedCount == 1;
    }

    public async Task<Order> AddOrder(Order order)
    {
        return await InsertOneAsync(order);
    }

    public async Task<IReadOnlyCollection<Order>> GetOrders()
    {
        return await Query().ToListAsync();
    }

    public async Task<Order?> GetOrderById(ObjectId id)
    {
        return await Query().Where(o => o.Id == id).FirstOrDefaultAsync();
    }

    public async Task<bool> DeleteOrder(ObjectId id)
    {
        var deleteResult = await DeleteOneAsync(id);
        return deleteResult.DeletedCount == 1;
    }

    public async Task<IReadOnlyCollection<OrderItem>> GetOrderItemsForOrder(ObjectId orderId)
    {
        return await Query().Where(o => o.Id == orderId).Select(o => o.OrderItems).FirstOrDefaultAsync();
    }

    public async Task<bool> AddOrderItem(ObjectId orderId, OrderItem orderItem)
    {
        var x = UpdateDefBuilder.Push(o => o.OrderItems, orderItem);
        var res = await UpdateOneAsync(orderId, x);
        return res is { IsAcknowledged: true, ModifiedCount: 1 };
    }


    public async Task<bool> DeleteOrderItemOfOrder(ObjectId orderId, OrderItem orderItem)
    {
        Order? order = await GetOrderById(orderId);
        if (order == default)
        {
            return await Task.FromResult(false);
        }

        bool removalSucceeded = order.OrderItems.Remove(orderItem);
        if (removalSucceeded == false)
        {
            return await Task.FromResult(false);
        }

        ReplaceOneResult? res = await ReplaceOneAsync(order);
        return res is { IsAcknowledged: true, ModifiedCount: 1 };
    }

    public async Task<bool> DeleteOrderItemsOfOrder(ObjectId orderId)
    {
        Order? order = await GetOrderById(orderId);
        if (order == default)
        {
            return await Task.FromResult(false);
        }

        order.OrderItems.Clear();

        ReplaceOneResult? res = await ReplaceOneAsync(order);
        return res is { IsAcknowledged: true, ModifiedCount: 1 };
    }
    
    public async Task<bool> DeleteOrdersOfCustomer(ObjectId customerId)
    {
        var res = await DeleteManyAsync(o => o.CustomerId == customerId);
        return res.IsAcknowledged;
    }
}
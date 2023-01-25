using LeoMongo.Database;
using LeoMongo.Transaction;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MongoDBDemoApp.Core.Workloads.Comments;

namespace MongoDBDemoApp.Core.Workloads.Order;

public sealed class CustomerRepository : RepositoryBase<Customer>,ICustomerRepository
{
    public override string CollectionName { get; } = LeoMongo.MongoUtil.GetCollectionName<Customer>();
    
    public async Task<Customer> AddCustomer(Customer customer)
    {
        return await InsertOneAsync(customer);
    }

    public async Task<List<Customer>> GetCustomers()
    {
        return await Query().ToListAsync();
    }

    public async Task<Customer?> GetCustomerById(ObjectId customerId)
    {
        return await Query().Where(o => o.Id == customerId).FirstOrDefaultAsync();
    }

    public async Task<bool> UpdateCustomer(Customer customer)
    {
        var updateDef = UpdateDefBuilder.Set(c => c.FirstName,customer.FirstName);
        updateDef.Set(c => c.LastName, customer.LastName);
        updateDef.Set(c => c.PhoneNumber, customer.PhoneNumber);
        var res = await UpdateOneAsync(customer.Id, updateDef);
        return res is { IsAcknowledged: true, ModifiedCount: 1 };
    }

    public async Task<bool> DeleteCustomer(ObjectId customerId)
    {
        var res = await DeleteOneAsync(customerId);
        return res is { IsAcknowledged: true, DeletedCount: 1 };
    }

    public CustomerRepository(ITransactionProvider transactionProvider, IDatabaseProvider databaseProvider) : base(transactionProvider, databaseProvider)
    {
        
    }
}
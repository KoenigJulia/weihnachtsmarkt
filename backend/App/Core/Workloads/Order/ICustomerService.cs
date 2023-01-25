using LeoMongo.Database;
using MongoDB.Bson;
using MongoDBDemoApp.Core.Workloads.Order;

namespace MongoDBDemoApp.Core.Workloads.Order;

public interface ICustomerService: IRepositoryBase
{
    Task<Customer> AddCustomer(Customer customer);
    Task<List<Customer>> GetCustomers();
    Task<Customer?> GetCustomerById(ObjectId customerId);
    Task<bool> UpdateCustomer(Customer customer);
    Task<bool> DeleteCustomer(ObjectId customerId);
}
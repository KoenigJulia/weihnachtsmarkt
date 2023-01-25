using LeoMongo.Database;
using MongoDB.Bson;

namespace MongoDBDemoApp.Core.Workloads.Customers;

public interface ICustomerRepository : IRepositoryBase
{
    Task<Customer> AddCustomer(Customer customer);
    Task<List<Customer>> GetCustomers();
    Task<Customer?> GetCustomerById(ObjectId customerId);
    Task<bool> UpdateCustomer(Customer customer);
    Task<bool> DeleteCustomer(ObjectId customerId);
}
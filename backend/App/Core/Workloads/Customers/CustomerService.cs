using LeoMongo;
using MongoDB.Bson;
using MongoDBDemoApp.Core.Util;

namespace MongoDBDemoApp.Core.Workloads.Customers;

public sealed class CustomerService : ICustomerService
{
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly ICustomerRepository _repository;

    public CustomerService(IDateTimeProvider dateTimeProvider, ICustomerRepository repository)
    {
        _dateTimeProvider = dateTimeProvider;
        _repository = repository;
    }
    
    public string CollectionName { get; } = MongoUtil.GetCollectionName<Customer>();
    public async Task<Customer> AddCustomer(Customer customer)
    {
        return await _repository.AddCustomer(customer);
    }

    public async Task<List<Customer>> GetCustomers()
    {
        return await _repository.GetCustomers();
    }

    public async Task<Customer?> GetCustomerById(ObjectId customerId)
    {
        return await _repository.GetCustomerById(customerId);
    }

    public async Task<bool> UpdateCustomer(Customer customer)
    {
        return await _repository.UpdateCustomer(customer);
    }

    public async Task<bool> DeleteCustomer(ObjectId customerId)
    {
        return await _repository.DeleteCustomer(customerId);
    }
}
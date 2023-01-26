using AutoMapper;
using MongoDBDemoApp.Core.Workloads.Customers;
using MongoDBDemoApp.Core.Workloads.Products;
using MongoDBDemoApp.Core.Workloads.Vendors;
using MongoDBDemoApp.Model.CreateCustomerRequest;
using MongoDBDemoApp.Model.Product;
using MongoDBDemoApp.Model.Vendor;

namespace MongoDBDemoApp.Core.Util;

public sealed class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<Vendor, VendorDto>()
            .ForMember(v => v.Id,
                m => m.MapFrom(v => v.Id.ToString()));
        
        CreateMap<Customer, CustomerDto>()
            .ForMember(c => c.Id, 
                m => m.MapFrom(c => c.Id.ToString()));
        
        CreateMap<Product, ProductDto>()
            .ForMember(p => p.Id, 
                m => m.MapFrom(p => p.Id.ToString()));
    }
}
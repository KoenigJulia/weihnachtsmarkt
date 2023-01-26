﻿using AutoMapper;
using MongoDBDemoApp.Core.Workloads.Customers;
using MongoDBDemoApp.Core.Workloads.Orders;
using MongoDBDemoApp.Core.Workloads.Products;
using MongoDBDemoApp.Core.Workloads.Vendors;
using MongoDBDemoApp.Model.CreateCustomerRequest;
using MongoDBDemoApp.Model.Order;
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

        CreateMap<Order, OrderDto>()
            .ForMember(o => o.Id,
                m => m.MapFrom(o => o.Id.ToString()))
            .ForMember(o => o.CustomerId,
                m => m.MapFrom(o => o.CustomerId.ToString()))
            .ForMember(o => o.OrderItems,
                m => m.MapFrom(o => o.OrderItems.Select(oi => oi.ToString())));
    }
}
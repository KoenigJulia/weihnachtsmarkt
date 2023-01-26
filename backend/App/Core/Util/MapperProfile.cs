using AutoMapper;
using MongoDBDemoApp.Core.Workloads.Vendors;
using MongoDBDemoApp.Model.Comment;
using MongoDBDemoApp.Model.Vendor;

namespace MongoDBDemoApp.Core.Util;

public sealed class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<Vendor, VendorDto>()
            .ForMember(p => p.Id, c => c.MapFrom(p => p.Id.ToString()));
    }
}
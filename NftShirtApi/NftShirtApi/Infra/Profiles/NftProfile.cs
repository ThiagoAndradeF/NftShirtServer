using AutoMapper;
using NftShirt.Server.Data;
using NftShirt.Server.Data.Entities;
using NftShirt.Server.Infra.IRepositories;
using NftShirt.Server.Infra.Models;

namespace  NftShirt.Server.Infra.Profiles;
public class NftProfile : Profile{
    public NftProfile(){
        CreateMap<Nft, NftDto >()
            .ReverseMap();

        CreateMap<Nft, NftWithCollectionDto>()
            .ForMember(dest => dest.Collection, opt => opt.MapFrom(src => src.Collection))
            .ReverseMap();
    }
}

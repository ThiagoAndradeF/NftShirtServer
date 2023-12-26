using AutoMapper;
using NftShirt.Server.Data;
using NftShirt.Server.Data.Entities;
using NftShirt.Server.Infra.Models;

namespace  NftShirt.Server.Infra.Profiles;
public class ColectionProfile : Profile{
   public ColectionProfile(){
        CreateMap<Collection, CollectionCreateDto>()
            .ForMember(dest => dest.Contract, opt => opt.MapFrom(src => src.Contract))
            .ReverseMap();
        CreateMap<Collection, CollectionDto>()
            .ForMember(dest => dest.Contract, opt => opt.MapFrom(src => src.Contract))
            .ReverseMap();
    }


}



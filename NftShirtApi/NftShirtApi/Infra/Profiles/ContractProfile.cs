using AutoMapper;
using NftShirt.Server.Data.Entities;
using NftShirt.Server.Infra.Models;

namespace  NftShirt.Server.Infra.Profiles;
public class ContractProfile : Profile{
    
    public ContractProfile(){
        CreateMap<Contract, ContractCreateDto>()
            .ReverseMap();
        CreateMap<Contract, ContractDto>()
            .ReverseMap();
    }
}



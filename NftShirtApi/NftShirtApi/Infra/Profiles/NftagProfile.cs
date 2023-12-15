using System.Runtime.InteropServices;
using AutoMapper;
using NftShirt.Server.Data;
using NftShirt.Server.Data.Entities;
using NftShirt.Server.Infra.IRepositories;
using NftShirt.Server.Infra.Models;
namespace  NftShirt.Server.Infra;
public class NftagProfile : Profile {


    public NftagProfile(){
        CreateMap<Nftag, NftagCreateDto >()
            .ReverseMap();
    }
}

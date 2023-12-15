using AutoMapper;
using NftShirt.Server.Data;
using NftShirt.Server.Infra.IRepositories;

namespace  NftShirt.Server.Infra.Repositories;
public class WalletRepository: IWalletRepository{
    private readonly NftShirtContext _context;
    private readonly IMapper _mapper;

    public WalletRepository(NftShirtContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
}
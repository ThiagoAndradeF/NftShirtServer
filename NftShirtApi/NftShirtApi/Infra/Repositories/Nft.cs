using AutoMapper;
using NftShirt.Server.Data;
using NftShirt.Server.Infra.IRepositories;

namespace  NftShirt.Server.Infra.Repositories;
public class NftRepository: INftRepository{
    private readonly NftShirtContext _context;
    private readonly IMapper _mapper;

    public NftRepository(NftShirtContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
}
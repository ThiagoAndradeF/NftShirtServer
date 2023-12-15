using AutoMapper;
using NftShirt.Server.Data;
using NftShirt.Server.Infra.IRepositories;
using NftShirt.Server.Infra.Models;

namespace  NftShirt.Server.Infra.Repositories;
public class NftagRepository : INftagRepository
{
    private readonly NftShirtContext _context;
    private readonly IMapper _mapper;

    public NftagRepository(NftShirtContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<bool> Create(NftagCreateDto Nftag)
    {
        throw new NotImplementedException();
    }
}
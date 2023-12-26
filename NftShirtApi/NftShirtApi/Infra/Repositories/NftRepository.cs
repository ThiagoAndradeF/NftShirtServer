using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NftShirt.Server.Data;
using NftShirt.Server.Infra.IRepositories;
using NftShirt.Server.Infra.Models;

namespace  NftShirt.Server.Infra.Repositories;
public class NftRepository: INftRepository{
    private readonly NftShirtContext _context;
    private readonly IMapper _mapper;

    public NftRepository(NftShirtContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task <NftWithCollectionDto> GetNftCompleteByIdAsync(string TokenId)
    {

        return _mapper.Map<NftWithCollectionDto>(
            await _context.Nfts
                .Include(n => n.Collection)
                .Include(n =>n.Collection.Contract)
                .FirstOrDefaultAsync(n => n.TokenId == TokenId));

    }
}

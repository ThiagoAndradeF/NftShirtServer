using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NftShirt.Server.Data;
using NftShirt.Server.Data.Entities;
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

    public Task<bool> CreateAsync(NftDto newNft)
    {
        var nftIgual = _context.Nfts
            .FirstOrDefault(c => (c.TokenId == newNft.TokenId) && (c.ColectionID == newNft.ColectionId));
        if (nftIgual == null)
        {
            if(newNft.TokenId!= null){
                _context.Nfts.Add(_mapper.Map<Nft>(newNft));
                return SaveChangesAsync();
            }
            throw new Exception("Não é possível adicionar uma nft sem tokenId");
        }
        else{
            throw new Exception("Você já cadastrou essa nft");
        }
    }
    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}

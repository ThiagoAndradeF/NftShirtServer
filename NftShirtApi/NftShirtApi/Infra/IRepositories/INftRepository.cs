using NftShirt.Server.Infra.Models;

namespace  NftShirt.Server.Infra.IRepositories;
public interface INftRepository{
    public Task <NftWithCollectionDto> GetNftCompleteByIdAsync(string TokenId);
    public Task <bool> CreateAsync(NftDto newNft);
    
}
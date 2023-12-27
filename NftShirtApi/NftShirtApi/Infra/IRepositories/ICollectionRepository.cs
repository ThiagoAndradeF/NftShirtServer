using NftShirt.Server.Infra.Models;

namespace  NftShirt.Server.Infra.IRepositories;
public interface ICollectionRepository{
    public Task<bool> CreateAsync( CollectionCreateDto newCollection);
    public Task<IEnumerable<CollectionDto>> ListCollectionsAsync();
}
namespace  NftShirt.Server.Infra.IRepositories;
public interface ICollectionRepository{
    public Task<bool> Create(CollectionCreateDto CollectionCreateDto);
}
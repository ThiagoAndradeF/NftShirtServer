using NftShirt.Server.Infra.Models;

namespace  NftShirt.Server.Infra.IRepositories;
public interface INftagRepository{
    public Task<bool> Create(NftagCreateDto Nftag);
}
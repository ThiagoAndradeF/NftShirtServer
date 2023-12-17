using NftShirt.Server.Infra.Models;

namespace  NftShirt.Server.Infra.IRepositories;
public interface IContractRepository{
    public Task<bool> Create(ContractCreateDto Contract);
}
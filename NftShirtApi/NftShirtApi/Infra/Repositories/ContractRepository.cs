using AutoMapper;
using NftShirt.Server.Data;
using NftShirt.Server.Data.Entities;
using NftShirt.Server.Infra.IRepositories;
using NftShirt.Server.Infra.Models;

namespace  NftShirt.Server.Infra.Repositories;
public class ContractRepository : IContractRepository{
    private readonly NftShirtContext _context;
    private readonly IMapper _mapper;

    public ContractRepository(NftShirtContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<bool> Create(ContractCreateDto Contract)
    {
        _context.Contracts.Add(_mapper.Map<Contract>(Contract));
        return await SaveChangesAsync();
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }

}
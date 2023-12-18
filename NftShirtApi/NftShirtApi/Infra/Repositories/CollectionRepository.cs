
using AutoMapper;
using NftShirt.Server.Data;
using NftShirt.Server.Data.Entities;
using NftShirt.Server.Infra.IRepositories;
using NftShirt.Server.Infra.Models;

namespace  NftShirt.Server.Infra.Repositories;
public class CollectionRepository : ICollectionRepository{
    private readonly NftShirtContext _context;
    private readonly IMapper _mapper;
    private readonly IContractRepository _contractRepository;
    public CollectionRepository(NftShirtContext context, IMapper mapper, IContractRepository contractRepository)
    {
        _context = context;
        _mapper = mapper;
        _contractRepository = contractRepository;
    }

    public Task<bool> CreateAsync(CollectionCreateDto newCollection)
    {
        var contract = _context.Contracts
            .FirstOrDefault(c => c.Adress == newCollection.Contract.Adress);
        if (contract == null)
        {
            if(newCollection.Contract!= null){
                _contractRepository.Create(newCollection.Contract);
                return CreateAsync(newCollection);
            }
            return SaveChangesAsync();
        }else{
            var nftCollection = newCollection;
            _context.Colections.Add(_mapper.Map<Collection>(newCollection));
            return SaveChangesAsync();
        }
    }
    
    public Task<bool> CreateComplete(CollectionCreateDto newCollection)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}
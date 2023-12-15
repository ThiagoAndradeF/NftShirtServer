using AutoMapper;
using NftShirt.Server.Data;
using NftShirt.Server.Infra.IRepositories;

namespace  NftShirt.Server.Infra.Repositories;
public class CollectionRepository : ICollectionRepository{
    private readonly NftShirtContext _context;
    private readonly IMapper _mapper;

    public CollectionRepository(NftShirtContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }


}
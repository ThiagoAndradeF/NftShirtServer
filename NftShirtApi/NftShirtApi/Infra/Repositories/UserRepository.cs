using AutoMapper;
using NftShirt.Server.Data;
using NftShirt.Server.Infra.IRepositories;

namespace  NftShirt.Server.Infra.Repositories;
public class  UserRepository: IUserRepository{
    private readonly NftShirtContext _context;
    private readonly IMapper _mapper;

    public UserRepository(NftShirtContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
}
using Microsoft.AspNetCore.Mvc;
using NftShirt.Server.Infra.IRepositories;
using NftShirt.Server.Infra.Models;

namespace NftShirtApi.Controllers
{
    [ApiController]
    [Route("api/collection")]
    
    public class CollectionController : ControllerBase
    {
        private readonly ICollectionRepository _collectionRepository;

        public CollectionController(ICollectionRepository collectionRepository)
        {
            _collectionRepository = collectionRepository;
        }
        [HttpPost]
        public async Task<ActionResult<bool>> Create( CollectionCreateDto newCollection)
        {
            var result = await _collectionRepository.CreateAsync(newCollection);
            
            if(result) return Ok(newCollection);
            return BadRequest(newCollection);
        }
    }
}

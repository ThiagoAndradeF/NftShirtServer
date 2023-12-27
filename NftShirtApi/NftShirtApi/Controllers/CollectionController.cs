using Microsoft.AspNetCore.Mvc;
using NftShirt.Server.Infra.IRepositories;
using NftShirt.Server.Infra.Models;

namespace NftShirtApi.Controllers
{
    [ApiController]
    [Route("api/collection")]
    
    public class CollectionController : ControllerBase
    {
        private readonly ICollectionRepository _repository;
    
        public CollectionController(ICollectionRepository collectionRepository)
        {
            _repository = collectionRepository;
        }
        [HttpPost]
        public async Task<ActionResult<bool>> Create( CollectionCreateDto newCollection)
        {
            var result = await _repository.CreateAsync(newCollection);
            
            if(result) return Ok(newCollection);
            return BadRequest(newCollection);
        }
        [HttpGet("/all")]
        public async Task<ActionResult<CollectionCreateDto>> GetAlllCollections(string storeMail)
        {
            try
            {
                var listColection = await _repository.ListCollectionsAsync();
                if (listColection == null)
                {
                    return NotFound();
                }
                return Ok(listColection);
            }
            catch (System.Exception ex)
            {
                
                throw new Exception( ex.Message);
            }
        }
    }
}

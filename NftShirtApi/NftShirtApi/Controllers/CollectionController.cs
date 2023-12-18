using Microsoft.AspNetCore.Mvc;
using NftShirt.Server.Infra.IRepositories;

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
        // [HttpPost]
        // public async Task<ActionResult<bool>> Create( order)
        // {
        //     var result = await _orderRepository.CreateOrderWithItemsAndServicesAsync(order);
            
        //     if(result) return Ok(order);
        //     return BadRequest(order);
        // }
    }
}

using Microsoft.AspNetCore.Mvc;
using NftShirt.Server.Infra.IRepositories;
using NftShirtApi.Infra.Blockchain;

namespace NftShirtApi.Controllers
{
    [ApiController]
    [Route("api/nft")]
    public class NftController : ControllerBase
    {
        private readonly INftRepository _repository;
        private readonly IPollygonNftService _polygonNftService;
    
        public NftController(INftRepository nftRepository, IPollygonNftService pollygonNftService)
        {
            _repository = nftRepository;
            _polygonNftService = pollygonNftService;
        }

        [HttpGet("{tokenId}")]
        public async Task<ActionResult<string>> GetAlllCollections(string storeMail)
        {
            try
            {
                var listColection = await _polygonNftService.GetTokenUriAsync();
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

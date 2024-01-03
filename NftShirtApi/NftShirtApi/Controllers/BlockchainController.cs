using Microsoft.AspNetCore.Mvc;

namespace NftShirtApi.Controllers
{
    [ApiController]
    [Route("api/blockchain")]
    public class BlockchainController : ControllerBase
    {
        public string  _connectionPollygon = "https://polygon-rpc.com";

    }
}
